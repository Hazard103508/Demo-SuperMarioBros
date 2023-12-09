using Mario.Commons.Structs;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.Star
{
    public class StarStateJumping : StarState
    {
        #region Constructor
        public StarStateJumping(Star star) : base(star)
        {
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

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (Star.gameObject.activeSelf && hitInfo.IsBlock)
                Star.Movable.SetJumpForce(0);
        }
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