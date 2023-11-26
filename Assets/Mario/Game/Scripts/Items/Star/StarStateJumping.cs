using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items.Star
{
    public class StarStateJumping : StarState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        #endregion

        #region Constructor
        public StarStateJumping(Star star) : base(star)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Star.Movable.enabled = true;
            Star.Movable.Speed = Star.Profile.MoveSpeed * Mathf.Sign(Star.Movable.Speed);
            Star.Movable.Gravity = Star.Profile.FallSpeed;
            Star.Movable.MaxFallSpeed = Star.Profile.MaxFallSpeed;
        }
        #endregion

        #region Private Methods
        protected override bool CollectStar(PlayerController player)
        {
            if (base.CollectStar(player))
            {
                _scoreService.Add(Star.Profile.Points);
                _scoreService.ShowPoints(Star.Profile.Points, Star.transform.position + Vector3.up * 1.75f, 0.8f, 3f);
                player.Buff();
                return true;
            }
            return false;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            if (Star.gameObject.activeSelf && hitInfo.IsBlock)
                Star.Movable.SetJumpForce(Star.Profile.BounceSpeed);
        }
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => CollectStar(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => CollectStar(player);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => CollectStar(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => CollectStar(player);
        #endregion
    }
}