using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateInShell : KoopaState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;

        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateInShell(Koopa koopa) : base(koopa)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region Public methods
        public override void OnGameFrozen()
        {
        }
        public override void OnGameUnfreezed()
        {
        }
        #endregion

        #region Private Methods
        private void OnHittedByPlayerFromSide(PlayerController player)
        {
            if (_timer > 0.1f)
            {
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateBouncing);
                ChangeSpeedAfferHit(player.transform.position);
            }
        }
        private void KillKoopa(Vector3 hitPosition)
        {
            Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateDead);
            ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Koopa.Movable.enabled = false;
            Koopa.Animator.SetTrigger("Hit");

            _scoreService.Add(Koopa.Profile.PointsHit1);
            _scoreService.ShowPoints(Koopa.Profile.PointsHit1, Koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
            _soundService.Play(Koopa.Profile.HitSoundFXPoolReference, Koopa.transform.position);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 4f)
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateWakingUp);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromLeft(PlayerController player) => OnHittedByPlayerFromSide(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => OnHittedByPlayerFromSide(player);
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => KillKoopa(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa koopa) => KillKoopa(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => KillKoopa(fireball.transform.position);
        #endregion
    }
}