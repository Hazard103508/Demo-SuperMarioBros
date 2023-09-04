using Mario.Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSuperIdle : PlayerStateIdle
    {
        #region Constructor
        public PlayerStateSuperIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            if (SetTransitionToDucking())
                return;

            base.Update();
        }
        #endregion
    }
}