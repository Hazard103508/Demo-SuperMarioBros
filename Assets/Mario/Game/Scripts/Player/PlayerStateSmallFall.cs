using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallFall : PlayerStateSmall
    {
        #region Constructor
        public PlayerStateSmallFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {

        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => SetTransitionToIdle();
        #endregion
    }
}