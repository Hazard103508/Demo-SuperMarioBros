using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateSmallJump : PlayerStateSmall
    {
        #region Objects
        private float _lastJumpPressed = 0;
        #endregion

        #region Properties
        private bool JumpMinBuffered => _lastJumpPressed + Player.Profile.Jump.MinBufferTime > Time.time;
        private bool JumpMaxBuffered
        {
            get
            {
                float absCurrentSpeed = Mathf.Abs(Player.Movable.Speed);
                if (absCurrentSpeed > Player.Profile.Walk.MaxSpeed)
                {
                    float speedFactor = Mathf.InverseLerp(Player.Profile.Walk.MaxSpeed, Player.Profile.Run.MaxSpeed, absCurrentSpeed);
                    float finalBufferTime = Mathf.Lerp(Player.Profile.Jump.MaxWalkBufferTime, Player.Profile.Jump.MaxRunBufferTime, speedFactor);
                    return _lastJumpPressed + finalBufferTime > Time.time;
                }
                else
                    return _lastJumpPressed + Player.Profile.Jump.MaxWalkBufferTime > Time.time;
            }
        }
        #endregion

        #region Constructor
        public PlayerStateSmallJump(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Private Methods
        private void Jump()
        {
            if (JumpMinBuffered || (Player.InputActions.Jump && JumpMaxBuffered))
            {
                float jumpForce = Player.Movable.JumpForce + Player.Profile.Jump.Acceleration * Time.deltaTime;
                jumpForce = Mathf.Clamp(jumpForce, 0, Player.Profile.Jump.MaxSpeed);
                Player.Movable.AddJumpForce(jumpForce);

                UnityEngine.Debug.Log(jumpForce);
            }
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallFall);
        }
        //private void Jump()
        //{
        //    //    if (JumpMinBuffered || (Player.InputActions.Jump && JumpMaxBuffered))
        //    //    {
        //    float jumpForce = Player.Movable.JumpForce + Player.Profile.Jump.Acceleration * Time.deltaTime;
        //    jumpForce = Mathf.Clamp(jumpForce, 0, Player.Profile.Jump.MaxSpeed);
        //    Player.Movable.AddJumpForce(jumpForce);
        //
        //    // calcualar altura maxima que tendra el salto para la fuerza y gravedad actuales
        //
        //    //    }
        //    //    else
        //    //        Player.StateMachine.TransitionTo(Player.StateMachine.StateSmallFall);
        //}
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _lastJumpPressed = 0;
            Player.Animator.CrossFade("Small_Jump", 0);
        }
        public override void Update()
        {
            if(_lastJumpPressed == 0)
                _lastJumpPressed = Time.time;

            Jump();
        }
        #endregion
    }
}