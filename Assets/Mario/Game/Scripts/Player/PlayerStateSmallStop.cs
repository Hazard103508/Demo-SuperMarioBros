using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallStop : PlayerState
    {
        #region Constructor
        public PlayerStateSmallStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        protected void SetStopSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed > 0;
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Stop", 0);
        }
        public override void Update()
        {
            SpeedDown();

            if (TransitionToIdle())
                return;

            SetStopSpriteDirection();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        #endregion
    }
}