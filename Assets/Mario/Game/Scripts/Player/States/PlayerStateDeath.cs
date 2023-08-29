using System.Collections;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateDeath : PlayerState
    {
        #region Constructor
        public PlayerStateDeath(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private IEnumerator PlayFall()
        {
            Player.Movable.enabled = false;
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.Profile.Fall.DeathFallSpeed;
            yield return new WaitForSeconds(0.25f);
            var _jumpForce = UnityShared.Helpers.MathEquations.Trajectory.GetVelocity(Player.Profile.Jump.DeathHeight, -Player.Movable.Gravity);
            Player.Movable.ChekCollisions = false;
            Player.Movable.AddJumpForce(_jumpForce);
            Player.Movable.enabled = true;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            Player.Animator.CrossFade("Death", 0);
            Player.StartCoroutine(PlayFall());
        }
        #endregion
    }
}