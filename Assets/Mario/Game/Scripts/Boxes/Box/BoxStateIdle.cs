using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateIdle : BoxState
    {
        #region Constructor
        public BoxStateIdle(Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Box.Animator.SetTrigger("Idle");
            Box.Movable.RemoveGravity();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (Box.IsLastJump)
                Box.StateMachine.TransitionTo(Box.StateMachine.StateLastJump);
            else
                Box.StateMachine.TransitionTo(Box.StateMachine.StateJump);
        }
        #endregion
    }
}