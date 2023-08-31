using Mario.Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateBigIdle : PlayerStateIdle
    {
        #region Constructor
        public PlayerStateBigIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            if (SetTransitionToDuck())
                return;

            base.Update();
        }
        #endregion

    }
}