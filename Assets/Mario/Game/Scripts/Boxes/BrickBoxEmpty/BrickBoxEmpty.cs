using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxEmpty
{
    public class BrickBoxEmpty : Box.Box
    {
        #region Properties
        new public BrickBoxEmptyProfile Profile => (BrickBoxEmptyProfile)base.Profile;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            base.StateMachine.StateIdle = new BrickBoxEmptyStateIdle(this);
        }
        #endregion
    }
}