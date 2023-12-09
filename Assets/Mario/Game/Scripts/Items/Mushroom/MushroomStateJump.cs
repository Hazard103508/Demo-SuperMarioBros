using Mario.Commons.Structs;
using Mario.Game.Player;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateJump : MushroomState
    {
        #region Constructor
        public MushroomStateJump(Mushroom mushroom) : base(mushroom)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Mushroom.Movable.SetJumpForce(Mushroom.Profile.JumpAcceleration);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        public override void OnHittedByPlayerFromRight(PlayerController player) => Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        #endregion  
    }
}