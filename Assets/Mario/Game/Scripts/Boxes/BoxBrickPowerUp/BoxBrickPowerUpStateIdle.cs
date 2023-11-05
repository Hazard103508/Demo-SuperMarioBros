using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class BoxBrickPowerUpStateIdle : BoxStateIdle
    {
        #region Properties
        new protected BoxBrickPowerUp Box => (BoxBrickPowerUp)base.Box;
        #endregion

        #region Constructor
        public BoxBrickPowerUpStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            Box.IsLastJump = true;
            if (player.StateMachine.CurrentMode.Equals(player.StateMachine.ModeSmall))
                Box.StateMachine.StateLastJump = new BoxBrickPowerUpStateLastJumpMushroom(Box);
            else
                Box.StateMachine.StateLastJump = new BoxBrickPowerUpStateLastJumpFlower(Box);

            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}