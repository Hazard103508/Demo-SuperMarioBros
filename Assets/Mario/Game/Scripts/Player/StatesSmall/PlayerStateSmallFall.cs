using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallFall : PlayerState
    {
        #region Constructor
        public PlayerStateSmallFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            if (Player.InputActions.Move.x == 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallIdle);
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallRun);
        }
        #endregion
    }
}