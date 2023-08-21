using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class InvisibleBox1UPStateLastJump : BoxStateLastJump
    {
        #region Properties
        new protected InvisibleBox1UP Box => (InvisibleBox1UP)base.Box;
        #endregion

        #region Constructor
        public InvisibleBox1UPStateLastJump(Box.Box box) : base(box)
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
            Services.PoolService.GetObjectFromPool(Box.Profile.MushroomPoolReference, Box.transform.position);
        }
        #endregion
    }
}