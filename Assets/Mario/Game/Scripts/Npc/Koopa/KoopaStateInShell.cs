using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateInShell : KoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateInShell(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            _koopa.Movable.Speed = 0;
            _koopa.Animator.SetTrigger("Hit");

            Services.ScoreService.Add(_koopa.Profile.PointsHit1);
            Services.ScoreService.ShowPoints(_koopa.Profile.PointsHit1, _koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);

            _koopa.PlayHitSoundFX();
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 4f)
                _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateWakingUp);
        }
        #endregion

        #region Private Methods
        private void OnHittedByPlayerFromSide(PlayerController player)
        {
            if (_timer > 0.1f)
            {
                _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateBouncing);
                _koopa.ChangeSpeedAfferHit(player.transform.position);
            }
        }
        private void KillKoopa(Vector3 hitPosition)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateDead);
            _koopa.ChangeSpeedAfferHit(hitPosition);
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