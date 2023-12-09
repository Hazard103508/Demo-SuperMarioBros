using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Helpers;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateDeath : PlayerState
    {
        #region Objects
        private readonly IPlayerService _playerService;
        private readonly ISoundService _soundService;
        private readonly IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public PlayerStateDeath(PlayerController player) : base(player)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Death";
        #endregion

        #region Private Methods
        private IEnumerator FallOutOffScreen()
        {
            Player.Renderer.sortingLayerName = "Dead";

            _gameplayService.State = GameplayService.GameState.Lose;
            _gameplayService.FreezeGame();
            Player.Movable.Speed = 0;
            Player.Movable.Gravity = Player.StateMachine.CurrentMode.ModeProfile.Fall.DeathFallSpeed;
            yield return new WaitForSeconds(0.25f);

            var _jumpForce = MathEquations.Trajectory.GetVelocity(Player.StateMachine.CurrentMode.ModeProfile.Jump.DeathHeight, -Player.Movable.Gravity);
            Player.Movable.ChekCollisions = false;
            Player.Movable.SetJumpForce(_jumpForce);
            Player.Movable.enabled = true;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            Player.StartCoroutine(FallOutOffScreen());
            _soundService.StopTheme();
            _playerService.RemoveLife();
        }
        public override void Exit()
        {
            base.Exit();
            ChangeModeToSmall(Player);
        }
        #endregion
    }
}