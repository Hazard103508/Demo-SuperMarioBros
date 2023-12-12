using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Interactable;
using Mario.Game.Player;
using System.Linq;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateBouncing : KoopaState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        private readonly IGameplayService _gameplayService;

        private float _timer = 0;
        #endregion

        #region Constructor
        public KoopaStateBouncing(Koopa koopa) : base(koopa)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Private Methods
        private void DamagePlayer(PlayerController player)
        {
            if (_timer > 0.15f)
                player.Hit(Koopa);
        }
        private void HitObjectBySide(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            hitInfo.IsBlock = hitInfo.hitObjects.Any(obj => obj.IsBlock);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Koopa.Animator.SetTrigger("Hit");
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.BouncingSpeed * GetDirection();

            _soundService.Play(Koopa.Profile.KickSoundFXPoolReference, Koopa.transform.position);
            _scoreService.Add(Koopa.Profile.PointsHit2);
            _scoreService.ShowPoints(Koopa.Profile.PointsHit2, Koopa.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            HitObjectBySide(hitInfo);
            if (hitInfo.IsBlock)
            {
                ChangeDirectionToRight();
                _soundService.Play(Koopa.Profile.BouncingSoundFXPoolReference, Koopa.transform.position);
            }
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            HitObjectBySide(hitInfo);
            if (hitInfo.IsBlock)
            {
                ChangeDirectionToLeft();
                _soundService.Play(Koopa.Profile.BouncingSoundFXPoolReference, Koopa.transform.position);
            }
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            if (_gameplayService.IsStarman)
            {
                Kill(player.transform.position);
                return;
            }

            if (_timer > 0.1f)
            {
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateInShell);
                player.BounceJump();
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => DamagePlayer(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => DamagePlayer(player);
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