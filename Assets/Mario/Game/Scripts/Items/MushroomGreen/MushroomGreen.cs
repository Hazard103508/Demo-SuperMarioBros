namespace Mario.Game.Items.GreenMushroom
{
    public class MushroomGreen : Mushroom.Mushroom
    {
        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateWalk = new MushroomGreenStateWalk(this);
        }
        #endregion
    }
}