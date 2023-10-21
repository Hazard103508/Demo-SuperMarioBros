using Mario.Game.Player;

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
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (Box.IsLastJump)
                Box.StateMachine.TransitionTo(Box.StateMachine.StateLastJump);
            else
                Box.StateMachine.TransitionTo(Box.StateMachine.StateJump);
        }
        #endregion
    }
}