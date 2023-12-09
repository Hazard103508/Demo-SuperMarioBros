using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateHit : GoombaState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        private float _timer = 0;
        #endregion

        #region Constructor
        public GoombaStateHit(Goomba goomba) : base(goomba)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Goomba.Movable.enabled = false;
            Goomba.gameObject.layer = 0;
            Goomba.Animator.SetTrigger("Hit");

            _soundService.Play(Goomba.Profile.HitSoundFXPoolReference);
            _scoreService.Add(Goomba.Profile.Points);
            _scoreService.ShowPoints(Goomba.Profile.Points, Goomba.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.4f)
                Goomba.gameObject.SetActive(false);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToLeft();
        }
        #endregion
    }
}