using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateDuckingJump : PlayerStateJump
    {
        #region Constructor
        public PlayerStateDuckingJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Ducking";
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetRaycastDucking();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => HitObjectOnTop(hitInfo.hitObjects);
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitObjectOnBottom(hitInfo.hitObjects);
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo) => HitObjectOnLeft(hitInfo.hitObjects);
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo) => HitObjectOnRight(hitInfo.hitObjects);
        #endregion
    }
}