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
            Box.IsLastJump = true;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (player.Mode == Enums.PlayerModes.Small)
                Box.StateMachine.StateLastJump = new MysteryBoxPowerUpStateLastJumpMushroom(Box);
            else
                Box.StateMachine.StateLastJump = new MysteryBoxPowerUpStateLastJumpFlower(Box);

            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}