using Mario.Application.Services;
using Mario.Game.Player;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateBouncing : KoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateBouncing(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            _koopa.Animator.SetTrigger("Hit");
            _koopa.Movable.enabled = true;
            _koopa.Movable.Speed = _koopa.Profile.BouncingSpeed;
            _koopa.PlayKickSoundFX();
            
            Services.ScoreService.Add(_koopa.Profile.PointsHit2);
            Services.ScoreService.ShowPoints(_koopa.Profile.PointsHit2, _koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
        }
        #endregion

        #region Private Methods
        private void DamagePlayer(PlayerController player)
        {
            if (_timer > 0.1f)
                player.DamagePlayer();
        }
        private void KillKoopa(Vector3 hitPosition)
        {
            _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateDead);
            _koopa.ChangeSpeedAfferHit(hitPosition);
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            _koopa.HitObject(hitInfo);
            hitInfo.IsBlock = hitInfo.hitObjects.Any(obj => obj.IsBlock);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (hitInfo.IsBlock)
            {
                _koopa.ChangeDirectionToRight(hitInfo);
                _koopa.PlayBlockSoundFX();
            }
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (hitInfo.IsBlock)
            {
                _koopa.ChangeDirectionToLeft(hitInfo);
                _koopa.PlayBlockSoundFX();
            }
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            if (_timer > 0.1f)
            {
                _koopa.StateMachine.TransitionTo(_koopa.StateMachine.StateInShell);
                player.BounceJump();
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => DamagePlayer(player);
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