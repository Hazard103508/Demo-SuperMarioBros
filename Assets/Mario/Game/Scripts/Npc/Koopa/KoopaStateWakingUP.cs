using Mario.Game.Interfaces;
using Mario.Game.Player;
using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWakingUp : IKoopaState
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
        public void Enter()
        {
            _timer = 0;
            _koopa.Animator.SetTrigger("WakeUp");
        }
        public void Exit()
        {
        }
        public void Update()
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

        #region IKoopaState Methods
        public void OnLeftCollided(RayHitInfo hitInfo) { }
        public void OnRightCollided(RayHitInfo hitInfo) { }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) { }
        public void OnHittedByPlayerFromLeft(PlayerController player) => OnHittedByPlayerFromSide(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => OnHittedByPlayerFromSide(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) { }
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => KillKoopa(box.transform.position);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) => KillKoopa(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => KillKoopa(fireball.transform.position);
        #endregion
    }
}