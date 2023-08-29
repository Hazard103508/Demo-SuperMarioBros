using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateRun : PlayerState
    {
        #region Constructor
        public PlayerStateRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Run", 0);
        }
        public override void Update()
        {
            SpeedUp();
            SpeedDown();
            SetAnimationSpeed();

            if (SetTransitionToJump())
                return;

            if (SetTransitionToFall())
                return;

            if (SetTransitionToStop())
                return;

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();
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