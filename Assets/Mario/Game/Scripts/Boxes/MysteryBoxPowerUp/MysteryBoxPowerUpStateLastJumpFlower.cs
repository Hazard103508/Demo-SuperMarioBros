using Mario.Application.Services;
using Mario.Game.Boxes.Box;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class MysteryBoxPowerUpStateLastJumpFlower : BoxStateLastJump
    {
        #region Properties
        new protected MysteryBoxPowerUp Box => (MysteryBoxPowerUp)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxPowerUpStateLastJumpFlower(Box.Box box) : base(box)
        {
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            Services.PoolService.GetObjectFromPool(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            Services.PoolService.GetObjectFromPool(Box.Profile.FlowerPoolReference, Box.transform.position);
        }
        #endregion
    }
}