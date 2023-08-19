using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWalk : IKoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        #endregion

        #region Constructor
        public KoopaStateWalk(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region IState Methods
        public void Enter()
        {
            _koopa.Movable.enabled = true;
            _koopa.Movable.Speed = _koopa.Profile.MoveSpeed;
            _koopa.Animator.SetTrigger("Idle");
        }
        public void Exit()
        {
        }
        public void Update()
        {
        }
        #endregion

        #region Private Methods
        private void KillKoopa(Vector3 hitPosition)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateDead);
            _koopa.ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region IKoopaState Methods
        public void OnLeftCollided(RayHitInfo hitInfo) => _koopa.ChangeDirectionToRight(hitInfo);
        public void OnRightCollided(RayHitInfo hitInfo) => _koopa.ChangeDirectionToLeft(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateInShell);
            player.BounceJump();
        }
        public void OnHittedByPlayerFromLeft(PlayerController player) => player.DamagePlayer();
        public void OnHittedByPlayerFromRight(PlayerController player) => player.DamagePlayer();
        public void OnHittedByPlayerFromBottom(PlayerController player) => player.DamagePlayer();
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