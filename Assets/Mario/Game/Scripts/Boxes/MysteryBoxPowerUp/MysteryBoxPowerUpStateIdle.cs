using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class MysteryBoxPowerUpStateIdle : BoxStateIdle
    {
        #region Properties
        new protected MysteryBoxPowerUp Box => (MysteryBoxPowerUp)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxPowerUpStateIdle(Box.Box box) : base(box)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            IsLastJump = true;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (player.StateMachine.CurrentMode.Equals(player.StateMachine.ModeSmall))
                Box.StateMachine.StateLastJump = new MysteryBoxPowerUpStateLastJumpMushroom(Box);
            else
                Box.StateMachine.StateLastJump = new MysteryBoxPowerUpStateLastJumpFlower(Box);

            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}