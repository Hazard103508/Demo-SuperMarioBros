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
        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateBouncing(Koopa koopa) : base(koopa)
        {
        }
        #endregion

        #region Private Methods
        private void DamagePlayer(PlayerController_OLD player)
        {
            if (_timer > 0.1f)
                player.DamagePlayer();
        }
        private void KillKoopa(Vector3 hitPosition)
        {
            Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateDead);
            Koopa.ChangeSpeedAfferHit(hitPosition);
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            Koopa.HitObject(hitInfo);
            hitInfo.IsBlock = hitInfo.hitObjects.Any(obj => obj.IsBlock);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Koopa.Animator.SetTrigger("Hit");
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.BouncingSpeed;
            Koopa.PlayKickSoundFX();

            Services.ScoreService.Add(Koopa.Profile.PointsHit2);
            Services.ScoreService.ShowPoints(Koopa.Profile.PointsHit2, Koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (hitInfo.IsBlock)
            {
                Koopa.ChangeDirectionToRight();
                Koopa.PlayBlockSoundFX();
            }
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            if (hitInfo.IsBlock)
            {
                Koopa.ChangeDirectionToLeft();
                Koopa.PlayBlockSoundFX();
            }
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController_OLD player)
        {
            if (_timer > 0.1f)
            {
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateInShell);
                player.BounceJump();
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController_OLD player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromRight(PlayerController_OLD player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player) => DamagePlayer(player);
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