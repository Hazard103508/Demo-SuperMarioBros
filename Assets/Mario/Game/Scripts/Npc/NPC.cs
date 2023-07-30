using Mario.Application.Services;
using Mario.Game.Boxes;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc
{
    public class NPC : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] private SquareRaycast _raycastRanges;
        [SerializeField] protected SpriteRenderer _renderer;
        [SerializeField] protected AudioSource _hitSoundFX;
        [SerializeField] protected AudioSource _kickSoundFX;
        [SerializeField] protected Animator _animator;

        protected Vector3 _currentSpeed;
        protected bool _isDead;
        protected Bounds<RayHitInfo> _proximityBlock = new();

        #region Unity Methods
        protected virtual void Awake()
        {
            AllServices.PlayerService.OnCanMoveChanged.AddListener(OnCanMoveChanged);

            _proximityBlock = new()
            {
                bottom = new(),
                left = new(),
                right = new(),
                top= new()
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

        #region Protected Properties
        public bool IsGrounded => _proximityBlock != null && _proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock;
        protected virtual float Profile_FallSpeed => 0;
        protected virtual float Profile_MaxFallSpeed => 0;
        protected virtual int Profile_PointsHit => 0;
        protected virtual int Profile_PointsKill => 0;
        protected virtual float Profile_JumpAcceleration => 0;
        #endregion

        #region Private Methods
        private void CalculateGravity()
        {
            _currentSpeed.y -= Profile_FallSpeed * Time.deltaTime;
            if (_proximityBlock.bottom != null && _proximityBlock.bottom.IsBlock)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -Profile_MaxFallSpeed)
                    _currentSpeed.y = -Profile_MaxFallSpeed;
            }
        }
        private void Kill(GameObject box)
        {
            _isDead = true;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            AllServices.ScoreService.Add(Profile_PointsKill);
            AllServices.ScoreService.ShowPoint(Profile_PointsKill, transform.position + Vector3.up * 1.5f, 0.8f, 3f);

            if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - box.transform.position.x))
                _currentSpeed.x *= -1;

            _currentSpeed.y = Profile_JumpAcceleration;

            _proximityBlock.bottom.IsBlock = false; // evito que colicione contra el suelo
            _proximityBlock.left.IsBlock = false;
            _proximityBlock.right.IsBlock = false;

            Destroy(_raycastRanges.gameObject);
            OnKill();
        }
        private IEnumerator OnHit()
        {
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
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
        #endregion

        #region Protected Methods
        protected virtual void CalculateWalk()
        {
            if (_proximityBlock.right.IsBlock)
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            else if (_proximityBlock.left.IsBlock)
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
        }
        protected virtual void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;
            transform.position = nextPosition;

            _raycastRanges.CalculateCollision(); // valido coliciones 
            
            SetHorizontalAlignment(ref nextPosition);
            SetVerticalAlignment(ref nextPosition);
            transform.position = nextPosition;
        }
        protected virtual void Hit(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            _hitSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(Profile_PointsHit);
            AllServices.ScoreService.ShowPoint(Profile_PointsHit, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            StartCoroutine(OnHit());

            player.BounceJump();
        }
        protected virtual void OnKill()
        { 
        }
        protected void DamagePlayer(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            player.DamagePlayer();
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = AllServices.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region External Events
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo)
        {
            _proximityBlock.bottom.IsBlock = hitInfo.IsBlock;
            foreach (var hit in hitInfo.hitObjects)
            {
                var box = hit.Object.GetComponent<BottomHitableBox>();
                if (box != null && box.IsJumping)
                {
                    Kill(hit.Object);
                    return;
                }
            };
        }
        #endregion

        #region On Player Hit
        public virtual void OnHitFromTop(PlayerController player) => Hit(player);
        public virtual void OnHitFromBottom(PlayerController player) => DamagePlayer(player);
        public virtual void OnHitFromLeft(PlayerController player) => DamagePlayer(player);
        public virtual void OnHitFromRight(PlayerController player) => DamagePlayer(player);
        #endregion
    }
}