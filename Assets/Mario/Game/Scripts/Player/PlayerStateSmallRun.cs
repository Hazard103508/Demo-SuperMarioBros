using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateSmallRun : PlayerState
    {
        #region Constructor
        public PlayerStateSmallRun(PlayerController player) : base(player)
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
            SpeedUp();
            SpeedDown();

            if (TransitionToIdle())
                return;

            SetAnimationSpeed();
            SetSpriteDirection();
        }
        #endregion

        #region Private Methods
        private void SpeedUp()
        {
            Player.Movable.Speed += Player.InputActions.Move.x * Player.Profile.Walk.Acceleration * Time.deltaTime;
            if (Mathf.Abs(Player.Movable.Speed) > Player.Profile.Walk.MaxSpeed)
            {
                float _speed = Player.InputActions.Sprint ? Player.Profile.Run.MaxSpeed : Player.Profile.Walk.MaxSpeed;
                Player.Movable.Speed = Mathf.Clamp(Player.Movable.Speed, -_speed, _speed);
            }
        }
        private void SpeedDown() 
        {
            if (Player.InputActions.Move.x == 0 || Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move.x))
            {
                float currentDeacceleration = Player.InputActions.Sprint ? Player.Profile.Run.Deacceleration : Player.Profile.Walk.Deacceleration;
                Player.Movable.Speed = Mathf.MoveTowards(Player.Movable.Speed, 0, currentDeacceleration * Time.deltaTime);
            }
        }
        private void SetAnimationSpeed()
        {
            float walkSpeedFactor = Mathf.Abs(Player.Movable.Speed) / Player.Profile.Walk.MaxSpeed;
            Player.Animator.speed = Mathf.Clamp(walkSpeedFactor, 0.5f, 1.5f);
        }
        private void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed < 0;
        private bool TransitionToIdle()
        {
            if (Player.Movable.Speed == 0)
            {
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallIdle);
                return true;
            }
            return false;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        #endregion
    }
}