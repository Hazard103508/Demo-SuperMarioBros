using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWalk : IKoopaState
    {
        private Koopa _koopa;

        public KoopaStateWalk(Koopa koopa)
        {
            _koopa = koopa;
        }

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

        #region IKoopaState Methods
        public void OnLeftCollided(RayHitInfo hitInfo) => _koopa.ChangeDirectionToRight(hitInfo);
        public void OnRightCollided(RayHitInfo hitInfo) => _koopa.ChangeDirectionToLeft(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player)
        {
            Services.ScoreService.Add(_koopa.Profile.PointsHit1);
            Services.ScoreService.ShowPoints(_koopa.Profile.PointsHit1, _koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);

            player.BounceJump();
            _koopa.PlayHitSoundFX();
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateInShell);
        }
        public void OnHittedByPlayerFromLeft(PlayerController player) => player.DamagePlayer();
        public void OnHittedByPlayerFromRight(PlayerController player) => player.DamagePlayer();
        public void OnHittedByPlayerFromBottom(PlayerController player) => player.DamagePlayer();
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => _koopa.Kill(box.transform.position);
        #endregion
    }
}