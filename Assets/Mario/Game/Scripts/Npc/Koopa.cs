using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc
{
    public class Koopa : MonoBehaviour,
        IHitableByPlayerFromTop,
        IHitableByPlayerFromBottom,
        IHitableByPlayerFromLeft,
        IHitableByPlayerFromRight,
        IHitableByBox,
        IHitableByKoppa
    {
        #region Objects
        [SerializeField] private KoopaProfile _profile;
        [SerializeField] private SquareRaycast _raycastRanges;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private AudioSource _blockSoundFX;
        [SerializeField] private Animator _animator;

        private Vector3 _currentSpeed;
        private bool _isDead;
        private Bounds<RayHitInfo> _proximityBlock = new();
        private Coroutine _wakingUpCO;
        private bool _hitCoolDown;
        #endregion

        #region Properties
        private bool IsGrounded => _proximityBlock != null && _proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock;
        private KoopaStates State { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            State = KoopaStates.Idle;
            SetSpeed();
            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;

            _proximityBlock = new()
            {
                bottom = new(),
                left = new(),
                right = new(),
                top = new()
            };
        }
        private void OnDestroy()
        {
            Services.PlayerService.CanMoveChanged -= OnCanMoveChanged;
        }
        private void Update()
        {
            if (!Services.PlayerService.CanMove)
                return;

            CalculateWalk();
            CalculateGravity();

            Move();
        }
        #endregion

        #region Public Methods
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region Private Methods
        private void Kill(Vector3 hitPosition)
        {
            if (!enabled || _isDead)
                return;

            _isDead = true;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            Services.ScoreService.Add(_profile.PointsHit1);
            Services.ScoreService.ShowPoints(_profile.PointsHit1, transform.position + Vector3.up * 2f, 0.8f, 3f);

            if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - hitPosition.x))
                _currentSpeed.x *= -1;

            _currentSpeed.y = _profile.JumpAcceleration;

            _proximityBlock.bottom.IsBlock = false; // evito que colicione contra el suelo
            _proximityBlock.left.IsBlock = false;
            _proximityBlock.right.IsBlock = false;

            Destroy(_raycastRanges.gameObject);

            if (_wakingUpCO != null)
                StopCoroutine(_wakingUpCO);

            State = KoopaStates.Idle;
            _renderer.transform.position += Vector3.up * 0.5f;
        }
        private void CalculateGravity()
        {
            _currentSpeed.y -= _profile.FallSpeed * Time.deltaTime;
            if (_proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -_profile.MaxFallSpeed)
                    _currentSpeed.y = -_profile.MaxFallSpeed;
            }
        }
        private void SetHorizontalAlignment(ref Vector3 nextPosition)
        {
            if (_proximityBlock.right.IsBlock && !_proximityBlock.left.IsBlock)
            {
                var hitObject = _proximityBlock.right.hitObjects.First();
                nextPosition.x = hitObject.Point.x - (0.5f + hitObject.RelativePosition.x);
            }
            else if (_proximityBlock.right.IsBlock || _proximityBlock.left.IsBlock)
            {
                var hitObject = _proximityBlock.left.hitObjects.First();
                nextPosition.x = hitObject.Point.x - (0.5f + hitObject.RelativePosition.x);
            }
        }
        private void SetVerticalAlignment(ref Vector3 nextPosition)
        {
            if (IsGrounded && _currentSpeed.y <= 0)
                nextPosition.y = Mathf.Round(nextPosition.y);
        }
        private void CalculateWalk()
        {
            if (_proximityBlock.right.IsBlock)
            {
                _renderer.flipX = false;
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            }
            else if (_proximityBlock.left.IsBlock)
            {
                _renderer.flipX = true;
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
            }
        }
        private void Move()
        {
            if (State == KoopaStates.InShell)
                return;

            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;
            transform.position = nextPosition;

            _raycastRanges.CalculateCollision(); // valido coliciones 

            SetHorizontalAlignment(ref nextPosition);
            SetVerticalAlignment(ref nextPosition);
            transform.position = nextPosition;
        }
        private void DamagePlayer(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            player.DamagePlayer();
        }
        private void HitFromTop(PlayerController player)
        {
            if (_hitCoolDown)
                return;

            if (State == KoopaStates.InShell)
                SecondHit(player);
            else
                FirstHit(player);
        }
        private void HitFromSide(PlayerController player)
        {
            if (_hitCoolDown)
                return;

            if (State == KoopaStates.InShell)
                SecondHit(player);
            else
                DamagePlayer(player);
        }
        private void FirstHit(PlayerController player)
        {
            if (_isDead)
                return;

            _hitSoundFX.Play();
            _animator.SetTrigger("Hit");

            Services.ScoreService.Add(_profile.PointsHit1);
            Services.ScoreService.ShowPoints(_profile.PointsHit1, transform.position + Vector3.up * 2f, 0.5f, 1.5f);

            State = KoopaStates.InShell;
            SetSpeed();
            _wakingUpCO = StartCoroutine(WakingUP());

            player.BounceJump();
            StartCoroutine(Cooldown());
        }
        private void SecondHit(PlayerController player)
        {
            if (_isDead)
                return;

            if (State == KoopaStates.InShell)
            {
                _animator.SetTrigger("Hit");

                if (_wakingUpCO != null)
                    StopCoroutine(_wakingUpCO);

                _kickSoundFX.Play();
                Services.ScoreService.Add(_profile.PointsHit2);
                Services.ScoreService.ShowPoints(_profile.PointsHit2, transform.position + Vector3.up * 2f, 0.5f, 1.5f);

                State = KoopaStates.Bouncing;
                SetSpeed();
                if (this.transform.position.x - player.transform.position.x < 0)
                    _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
                else
                    _currentSpeed.x = Mathf.Abs(_currentSpeed.x);

                StartCoroutine(Cooldown());
            }
        }
        private IEnumerator WakingUP()
        {
            yield return new WaitForSeconds(4f);
            _animator.SetTrigger("WakeUp");
            yield return new WaitForSeconds(1.5f);
            _animator.SetTrigger("Idle");

            State = KoopaStates.Idle;
        }
        private IEnumerator Cooldown()
        {
            _hitCoolDown = true;
            yield return new WaitForSeconds(.1f);
            _hitCoolDown = false;
        }
        private void SetSpeed()
        {
            float _direction = _renderer.flipX ? 1 : -1;
            _currentSpeed = (State == KoopaStates.Bouncing ? Math.Abs(_profile.BouncingSpeed) : Math.Abs(_profile.MoveSpeed)) * _direction * Vector2.right;
        }
        private void HitToLeft(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            _proximityBlock.left = hitInfo;
            PlayBlockSoundFX(_proximityBlock.left);
        }
        private void HitToRight(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            _proximityBlock.right = hitInfo;
            PlayBlockSoundFX(_proximityBlock.right);
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            if (State == KoopaStates.Bouncing)
            {
                if (hitInfo.hitObjects.Any())
                {
                    var hitObj = hitInfo.hitObjects.Select(hit => new
                    {
                        HitInfo = hit,
                        HittedObject = hit.Object.GetComponent<IHitableByKoppa>()
                    })
                    .ToList();

                    hitInfo.IsBlock = hitInfo.IsBlock && hitObj.Any(hit => hit.HittedObject == null);
                    hitObj.ForEach(hit => hit.HittedObject?.OnHittedByKoppa(this));
                }
            }
        }
        private void PlayBlockSoundFX(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock && this.State == KoopaStates.Bouncing)
                _blockSoundFX.Play();
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On local Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => HitToLeft(hitInfo);
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => HitToRight(hitInfo);
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo)
        {
            if (_isDead)
            {
                _proximityBlock.bottom.hitObjects = new System.Collections.Generic.List<HitObject>();
                _proximityBlock.bottom.IsBlock = false;
                return;
            }

            _proximityBlock.bottom.IsBlock = hitInfo.IsBlock;
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => HitFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => HitFromSide(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => HitFromSide(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => DamagePlayer(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa)
        {
            if (this.State == KoopaStates.Bouncing)
                koopa.Kill(this.transform.position);

            this.Kill(koopa.transform.position);
        }
        #endregion

        #region Structures
        public enum KoopaStates
        {
            Idle,
            InShell,
            Bouncing,
        }
        #endregion
    }
}