using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using UnityEngine;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class InvisibleBox1UPStateLastJump : BoxStateLastJump
    {
        #region Objects
        private readonly IPoolService _poolService;
        #endregion

        #region Properties
        new protected InvisibleBox1UP Box => (InvisibleBox1UP)base.Box;
        #endregion

        #region Constructor
        public InvisibleBox1UPStateLastJump(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            Box.gameObject.layer = LayerMask.NameToLayer("Ground");
            _poolService.GetObjectFromPool(Box.Profile.RiseItemSoundFXPoolReference, Box.transform.position);
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