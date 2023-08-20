using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallIdle : PlayerState
    {
        #region Constructor
        public PlayerStateSmallIdle(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Idle", 0);
        }
        #endregion
    }
}