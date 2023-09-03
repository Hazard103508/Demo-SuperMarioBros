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
            Player.Movable.Gravity = Player.StateMachine.CurrentMode.ModeProfile.Fall.DeathFallSpeed;
            yield return new WaitForSeconds(0.25f);
            var _jumpForce = UnityShared.Helpers.MathEquations.Trajectory.GetVelocity(Player.StateMachine.CurrentMode.ModeProfile.Jump.DeathHeight, -Player.Movable.Gravity);
            Player.Movable.ChekCollisions = false;
            Player.Movable.AddJumpForce(_jumpForce);
            Player.Movable.enabled = true;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            SetAnimatorState("Death");
            Player.StartCoroutine(PlayFall());
        }
        #endregion
    }
}