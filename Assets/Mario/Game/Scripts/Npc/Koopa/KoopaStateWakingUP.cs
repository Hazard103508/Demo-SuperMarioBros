using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWakingUp : KoopaState
    {
        #region Objects
        private float _timer = 0;
        private readonly IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public KoopaStateWakingUp(Koopa koopa) : base(koopa)
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Private Methods
        private void OnHittedByPlayerFromSide(PlayerController player)
        {
            if (_gameplayService.IsStarman)
            {
                Kill(player.transform.position);
                return;
            }

            if (_timer > 0.1f)
            {
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateBouncing);
                ChangeSpeedAfferHit(player.transform.position);
            }
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Koopa.Animator.SetTrigger("WakeUp");
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1.5f)
            {
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateWalk);
                if (Koopa.Renderer.flipX)
                    ChangeDirectionToLeft();
                else
                    ChangeDirectionToRight();
            }
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromLeft(PlayerController player) => OnHittedByPlayerFromSide(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => OnHittedByPlayerFromSide(player);
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa koopa) => Kill(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => Kill(fireball.GetHitPosition());
        #endregion
    }
}