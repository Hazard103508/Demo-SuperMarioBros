namespace Mario.Game.Items.GreenMushroom
{
    public class GreenMushroom : Mushroom.Mushroom
    {
        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateWalk = new GreenMushroomStateWalk(this);
        }
        #endregion
    }
}