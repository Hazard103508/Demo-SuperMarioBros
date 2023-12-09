using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateFire : PlayerState
    {
        #region Object
        private IPlayerService _playerService;

        private float _timer;
        #endregion

        #region Constructor
        public PlayerStateFire(PlayerController player) : base(player)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState()
        {
            return
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run2 ? "Fire_Run2" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Run3 ? "Fire_Run3" :
                Player.CurrentAnimationKey == PlayerAnimator.PlayerAnimationKeys.Jump ? "Fire_Jump" :
                "Fire_Run1";
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > 0.1f)
            {
                if (Player.StateMachine.GetPreviousStateType().IsAssignableFrom(typeof(PlayerStateJump)))
                    Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateFall);
                else
                    Player.StateMachine.TransitionToPreviousState();
            }
        }
        #endregion

        #region State Machine
        public override void Enter()
        {
            base.Enter();
            _timer = 0;
            _playerService.ShootFireball();
        }
        #endregion
    }
}