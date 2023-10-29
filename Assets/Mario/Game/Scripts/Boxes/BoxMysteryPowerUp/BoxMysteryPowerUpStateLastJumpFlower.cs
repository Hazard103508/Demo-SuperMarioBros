using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;

namespace Mario.Game.Boxes.MysteryBoxPowerUp
{
    public class BoxMysteryPowerUpStateLastJumpFlower : BoxStateLastJump
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BoxMysteryPowerUp Box => (BoxMysteryPowerUp)base.Box;
        #endregion

        #region Constructor
        public BoxMysteryPowerUpStateLastJumpFlower(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            _soundService.Play(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
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