using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateFall : PlayerState
    {
        #region Constructor
        public PlayerStateFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            if (Player.InputActions.Move.x == 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
        }
        #endregion
    }
}