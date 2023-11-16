using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxEmpty
{
    public class BoxBrickEmptyStateJump : BoxStateJump
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        private readonly IPlayerService _playerService;
        #endregion

        #region Properties
        new protected BoxBrickEmpty Box => (BoxBrickEmpty)base.Box;
        #endregion

        #region Constructor
        public BoxBrickEmptyStateJump(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();

            if (!_playerService.IsPlayerSmall())
            {
                _poolService.GetObjectFromPool(Box.Profile.BrokenBrickPoolReference, Box.transform.position);
                _scoreService.Add(Box.Profile.Points);
                _soundService.Play(Box.Profile.BreakSoundFXPoolReference, Box.transform.position);
                Box.StartCoroutine(Deactivate());
            }
        }
        #endregion

        #region Private Methods
        private IEnumerator Deactivate()
        {
            Box.Renderer.enabled = false;
            yield return new WaitUntil(() => Box.Movable.JumpForce <= 0);
            Box.gameObject.SetActive(false);
        }
        #endregion
    }
}