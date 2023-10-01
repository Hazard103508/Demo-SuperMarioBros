using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class MysteryBoxPowerUpStateLastJumpFlower : BoxStateLastJump
    {
        #region Objects
        private readonly IPoolService _poolService;
        #endregion

        #region Properties
        new protected MysteryBoxPowerUp Box => (MysteryBoxPowerUp)base.Box;
        #endregion

        #region Constructor
        public MysteryBoxPowerUpStateLastJumpFlower(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            _poolService.GetObjectFromPool(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            _poolService.GetObjectFromPool(Box.Profile.FlowerPoolReference, Box.transform.position);
        }
        #endregion
    }
}