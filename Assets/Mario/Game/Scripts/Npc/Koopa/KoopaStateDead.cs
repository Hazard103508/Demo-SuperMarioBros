using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateDead : KoopaState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public KoopaStateDead(Koopa koopa) : base(koopa)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Koopa.Movable.ChekCollisions = false;
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.MoveSpeed * GetDirection();
            Koopa.gameObject.layer = 0;
            Koopa.Movable.SetJumpForce(Koopa.Profile.JumpAcceleration);
            Koopa.Animator.SetTrigger("Kill");
            Koopa.Renderer.sortingLayerName = "Dead";
            Koopa.Renderer.transform.position += Vector3.up;

            _soundService.Play(Koopa.Profile.KickSoundFXPoolReference, Koopa.transform.position);
            _scoreService.Add(Koopa.Profile.PointsKill);
            _scoreService.ShowPoints(Koopa.Profile.PointsKill, Koopa.transform.position + Vector3.up * 2f, 0.8f, 3f);
        }
        #endregion
    }
}