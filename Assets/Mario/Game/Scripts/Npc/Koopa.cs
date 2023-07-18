using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Npc
{
    public class Koopa : NPC
    {
        [SerializeField] private KoopaProfile _koopaProfile;
        private bool _isInsideShell;

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            _currentSpeed = Vector2.right * _koopaProfile.MoveSpeed;
        }
        #endregion

        #region Protected Properties
        protected override float Profile_FallSpeed => _koopaProfile.FallSpeed;
        protected override float Profile_MaxFallSpeed => _koopaProfile.MaxFallSpeed;
        protected override int Profile_PointsHit => _koopaProfile.PointsHit;
        protected override float Profile_JumpAcceleration => _koopaProfile.JumpAcceleration;
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
            if (!enabled || _isKicked)
                return;

            _hitSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(Profile_PointsHit);
            AllServices.ScoreService.ShowPoint(Profile_PointsHit, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            player.BounceJump();
        }
        #endregion
    }
}