using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWakingUp : KoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateWakingUp(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            _koopa.Animator.SetTrigger("WakeUp");
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 1.5f)
                _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateWalk);
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