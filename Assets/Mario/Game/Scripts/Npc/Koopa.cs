using Mario.Application.Services;
using Mario.Game.Commons;
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
        IHitableByKoppa,
        IHitableByFireBall
    {
        #region Objects
        [SerializeField] private KoopaProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private AudioSource _blockSoundFX;
        [SerializeField] private Animator _animator;

        private Movable _movable;
        private Coroutine _wakingUpCO;
        private bool _hitCoolDown;
        #endregion

        #region Properties
        private KoopaStates State { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            State = KoopaStates.Idle;
            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;

            _movable = GetComponent<Movable>();
            _movable.Speed = _profile.MoveSpeed;
            _movable.Gravity = _profile.FallSpeed;
            _movable.MaxFallSpeed = _profile.MaxFallSpeed;

            SetSpeed();
        }
        private void OnDestroy()
        {
            Services.PlayerService.CanMoveChanged -= OnCanMoveChanged;
        }
        #endregion

        #region Public Methods
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region Private Methods
        private void Kill(Vector3 hitPosition)
        {
            _movable.ChekCollisions = false;
            gameObject.layer = 0;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            Services.ScoreService.Add(_profile.PointsHit1);
            Services.ScoreService.ShowPoints(_profile.PointsHit1, transform.position + Vector3.up * 2f, 0.8f, 3f);

            if (Math.Sign(_movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
                _movable.Speed *= -1;

            _movable.AddJumpForce(_profile.JumpAcceleration);

            if (_wakingUpCO != null)
                StopCoroutine(_wakingUpCO);

            State = KoopaStates.Idle;
            _renderer.transform.position += Vector3.up * 0.5f;
        }
        private void DamagePlayer(PlayerController player)
        {
            //if (!enabled || _isDead)
            //    return;

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
                    _movable.Speed = -Mathf.Abs(_movable.Speed);
                else
                    _movable.Speed = Mathf.Abs(_movable.Speed);

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
            float _direction = Mathf.Sign(_movable.Speed);
            _movable.Speed = (State == KoopaStates.Bouncing ? Math.Abs(_profile.BouncingSpeed) : Math.Abs(_profile.MoveSpeed)) * _direction;
        }
        private void HitToLeft(RayHitInfo hitInfo)
        {
            //HitObject(hitInfo);
            //_proximityBlock.left = hitInfo;
            //PlayBlockSoundFX(_proximityBlock.left);
        }
        private void HitToRight(RayHitInfo hitInfo)
        {
            //HitObject(hitInfo);
            //_proximityBlock.right = hitInfo;
            //PlayBlockSoundFX(_proximityBlock.right);
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
        private void ChangeDirectionToRight(RayHitInfo hitInfo)
        {
            if (gameObject.layer == 0)
                return;

            if (hitInfo.IsBlock)
            {
                _renderer.flipX = true;
                _movable.Speed = Mathf.Abs(_movable.Speed);
            }
        }
        private void ChangeDirectionToLeft(RayHitInfo hitInfo)
        {
            if (gameObject.layer == 0)
                return;

            if (hitInfo.IsBlock)
            {
                _renderer.flipX = false;
                _movable.Speed = -Mathf.Abs(_movable.Speed);
            }
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On local Ray Range Hit
        public void OnLeftCollided(RayHitInfo hitInfo) => ChangeDirectionToRight(hitInfo);
        public void OnRightCollided(RayHitInfo hitInfo) => ChangeDirectionToLeft(hitInfo);
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

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => Kill(fireball.transform.position);
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