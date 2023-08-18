using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWakingUp : IKoopaState
    {
        private Koopa _koopa;
        private float _timer = 0;

        public KoopaStateWakingUp(Koopa koopa)
        {
            _koopa = koopa;
        }

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

        #region IKoopaState Methods
        public void OnLeftCollided(RayHitInfo hitInfo) { }
        public void OnRightCollided(RayHitInfo hitInfo) { }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) { }
        public void OnHittedByPlayerFromLeft(PlayerController player) { }
        public void OnHittedByPlayerFromRight(PlayerController player) { }
        public void OnHittedByPlayerFromBottom(PlayerController player) { }
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => _koopa.Kill(box.transform.position);
        #endregion
    }
}