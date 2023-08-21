using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerState
    {
        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Idle", 0);
        }
        public override void Update()
        {
            if (Player.InputActions.Move.x != 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallWalk);
        }
        #endregion
    }
}