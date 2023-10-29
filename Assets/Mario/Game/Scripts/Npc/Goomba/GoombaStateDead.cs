using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateDead : GoombaState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public GoombaStateDead(Goomba goomba) : base(goomba)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Goomba.Movable.ChekCollisions = false;
            Goomba.Movable.enabled = true;
            Goomba.Movable.SetJumpForce(Goomba.Profile.JumpAcceleration);
            Goomba.Renderer.sortingLayerName = "Dead";
            Goomba.Renderer.transform.position += Vector3.up * 0.5f;
            Goomba.Animator.SetTrigger("Kill");
            Goomba.gameObject.layer = 0;

            _soundService.Play(Goomba.Profile.KickSoundFXPoolReference);
            _scoreService.Add(Goomba.Profile.Points);
            _scoreService.ShowPoints(Goomba.Profile.Points, Goomba.transform.position + Vector3.up * 2f, 0.8f, 3f);
        }
        #endregion
    }
}