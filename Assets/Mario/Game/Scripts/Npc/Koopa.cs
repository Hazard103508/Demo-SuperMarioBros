using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Npc
{
    public class Koopa : NPC
    {
        [SerializeField] private KoopaProfile _profile;
        private bool _isInsideShell;

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            _currentSpeed = Vector2.right * _profile.MoveSpeed;
        }
        #endregion

        #region Protected Properties
        protected override float Profile_FallSpeed => _profile.FallSpeed;
        protected override float Profile_MaxFallSpeed => _profile.MaxFallSpeed;
        protected override int Profile_PointsHit => _profile.PointsHit;
        protected override int Profile_PointsKill => _profile.PointsKill;
        protected override float Profile_JumpAcceleration => _profile.JumpAcceleration;
        #endregion

        #region Protected Methods
        protected override void CalculateWalk()
        {
            if (_proximityBlock.right)
            {
                _renderer.flipX = false;
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            }
            else if (_proximityBlock.left)
            {
                _renderer.flipX = true;
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
            }
        }
        protected override void Move()
        {
            if (_isInsideShell)
                return;

            base.Move();
        }
        protected override void Hit(PlayerController player)
        {
            if (!enabled || _isDead)
                return;

            if (!_isInsideShell)
                _isInsideShell = true;


            _hitSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(Profile_PointsHit);
            AllServices.ScoreService.ShowPoint(Profile_PointsHit, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            player.BounceJump();
        }
        protected override void OnKill()
        {
            _renderer.transform.position -= Vector3.up * 0.5f;
        }
        #endregion
    }
}