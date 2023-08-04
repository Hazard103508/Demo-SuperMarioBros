using Mario.Application.Services;
using Mario.Game.Boxes;
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
    public class Goomba : MonoBehaviour, IHitableByPlayerFromTop, IHitableByPlayerFromBottom, IHitableByPlayerFromLeft, IHitableByPlayerFromRight, IHitableByBoxFromBottom, IHitableByKoppa
    {
        #region Objects
        [SerializeField] private GoombaProfile _profile;
        [SerializeField] private SquareRaycast _raycastRanges;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private Animator _animator;

        private Vector3 _currentSpeed;
        private bool _isDead;
        private Bounds<RayHitInfo> _proximityBlock = new();
        #endregion

        #region Properties
        private bool IsGrounded => _proximityBlock != null && _proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.right * _profile.MoveSpeed;
            AllServices.PlayerService.OnCanMoveChanged.AddListener(OnCanMoveChanged);

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
            AllServices.PlayerService.OnCanMoveChanged.RemoveListener(OnCanMoveChanged);
        }
        private void Update()
        {
            if (!AllServices.PlayerService.CanMove)
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
        private void Kill(Vector3 hitPosition)
        {
            if (!enabled || _isDead)
                return;

            _isDead = true;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            AllServices.ScoreService.Add(_profile.Points);
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 3f);

            if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - hitPosition.x))
                _currentSpeed.x *= -1;

            _currentSpeed.y = _profile.JumpAcceleration;

            _proximityBlock.bottom.IsBlock = false; // evito que colicione contra el suelo
            _proximityBlock.left.IsBlock = false;
            _proximityBlock.right.IsBlock = false;

            Destroy(_raycastRanges.gameObject);
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
            if (_isDead)
                return;

            if (_proximityBlock.right.IsBlock)
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            else if (_proximityBlock.left.IsBlock)
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
        }
        private void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;
            transform.position = nextPosition;

            _raycastRanges.CalculateCollision();

            SetHorizontalAlignment(ref nextPosition);
            SetVerticalAlignment(ref nextPosition);
            transform.position = nextPosition;
        }
        private void Hit(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            _hitSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(_profile.Points);
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            player.BounceJump();
        }
        private void DamagePlayer(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            player.DamagePlayer();
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = AllServices.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On local Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo;
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
        public void OnHitableByPlayerFromTop(PlayerController player) => Hit(player);
        public void OnHitableByPlayerFromBottom(PlayerController player) => DamagePlayer(player);
        public void OnHitableByPlayerFromLeft(PlayerController player) => DamagePlayer(player);
        public void OnHitableByPlayerFromRight(PlayerController player) => DamagePlayer(player);
        #endregion

        #region On Box Hit
        public void OnIHitableByBoxFromBottom(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public void OnIHitableByKoppa(GameObject koopa) => Kill(koopa.transform.position);
        #endregion
    }
}