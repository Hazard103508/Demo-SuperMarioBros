using Mario.Game.Interfaces;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public abstract class PlayerState :
        IState,
        IHittableByMovingToTop,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight
    {
        #region State Machine
        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
        }
        #endregion

        #region Properties
        protected PlayerController Player { get; private set; }
        #endregion

        #region Constructor
        public PlayerState(PlayerController player)
        {
            Player = player;
        }
        #endregion

        #region Public Methods
        public virtual void OnBuff()
        {
        }
        public virtual void OnNerf()
        {
        }
        public virtual void OnDeath()
        {
        }
        #endregion

        #region Protected Methods
        protected void SpeedUp()
        {
            if (Player.InputActions.Move.x != 0)
            {
                float currentAcceleration = Player.InputActions.Sprint ? Player.Profile.Run.Acceleration : Player.Profile.Walk.Acceleration;
                Player.Movable.Speed += Player.InputActions.Move.x * currentAcceleration * Time.deltaTime;

                float _speed = Player.InputActions.Sprint ? Player.Profile.Run.MaxSpeed : Player.Profile.Walk.MaxSpeed;
                Player.Movable.Speed = Mathf.Clamp(Player.Movable.Speed, -_speed, _speed);
            }
        }
        protected void SpeedDown()
        {
            if (Player.InputActions.Move.x == 0 || Mathf.Sign(Player.Movable.Speed) != Mathf.Sign(Player.InputActions.Move.x))
            {
                float currentDeacceleration = Player.InputActions.Sprint ? Player.Profile.Run.Deacceleration : Player.Profile.Walk.Deacceleration;
                Player.Movable.Speed = Mathf.MoveTowards(Player.Movable.Speed, 0, currentDeacceleration * Time.deltaTime);
            }
        }
        protected void SetAnimationSpeed()
        {
            float walkSpeedFactor = Mathf.Abs(Player.Movable.Speed) / Player.Profile.Walk.MaxSpeed;
            Player.Animator.speed = Mathf.Clamp(walkSpeedFactor, 0.5f, 1.5f);
        }
        protected void ResetAnimationSpeed() => Player.Animator.speed = 1;
        protected virtual void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed < 0;
        #endregion

        #region On Movable Hit
        public virtual void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
        }
        public virtual void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
        }
        #endregion
    }
}
