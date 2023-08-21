using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class InvisibleBox1UP : Box.Box
    {
        #region Properties
        new public InvisibleBox1UPProfile Profile => (InvisibleBox1UPProfile)base.Profile;
        #endregion


        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new InvisibleBox1UPStateIdle(this);
            base.StateMachine.StateLastJump = new InvisibleBox1UPStateLastJump(this);
        }
        #endregion
    }
}