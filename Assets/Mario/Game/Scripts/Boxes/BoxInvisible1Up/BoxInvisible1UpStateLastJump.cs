using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using UnityEngine;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class BoxInvisible1UpStateLastJump : BoxStateLastJump
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BoxInvisible1Up Box => (BoxInvisible1Up)base.Box;
        #endregion

        #region Constructor
        public BoxInvisible1UpStateLastJump(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            Box.gameObject.layer = LayerMask.NameToLayer("Ground");
            _soundService.Play(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            _poolService.GetObjectFromPool(Box.Profile.MushroomPoolReference, Box.transform.position);
        }
        #endregion
    }
}