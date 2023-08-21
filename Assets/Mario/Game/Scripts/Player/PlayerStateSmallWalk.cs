using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallWalk : PlayerState
    {
        #region Constructor
        public PlayerStateSmallWalk(PlayerController player) : base(player)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Player.Animator.CrossFade("Small_Run", 0);
        }
        public override void Update()
        {
            if (Player.InputActions.Move.x == 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallIdle);
                return;
            }

            Player.Movable.Speed += Player.InputActions.Move.x * Player.Profile.Walk.Acceleration * Time.deltaTime;
            if(Mathf.Abs(Player.Movable.Speed) > Player.Profile.Walk.MaxSpeed)
                Player.Movable.Speed = Mathf.Clamp(Player.Movable.Speed, -Player.Profile.Walk.MaxSpeed, Player.Profile.Walk.MaxSpeed);
        }
        #endregion
    }
}