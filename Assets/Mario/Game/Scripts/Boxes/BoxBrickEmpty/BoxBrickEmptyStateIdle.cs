using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Boxes.BrickBoxEmpty
{
    public class BoxBrickEmptyStateIdle : BoxStateIdle
    {
        #region Objects
        private readonly IPoolService _poolService;
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        new protected BoxBrickEmpty Box => (BoxBrickEmpty)base.Box;
        #endregion

        #region Constructor
        public BoxBrickEmptyStateIdle(Box.Box box) : base(box)
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region Private Methods
        private IEnumerator DestroyBox()
        {
            yield return new WaitForEndOfFrame(); // wait state changed to jump
            yield return new WaitForEndOfFrame(); // wait for collition over the box
            Object.Destroy(Box.gameObject);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            base.OnHittedByPlayerFromBottom(player);
            if (!player.StateMachine.CurrentMode.Equals(player.StateMachine.ModeSmall))
            {
                _poolService.GetObjectFromPool(Box.Profile.BrokenBrickPoolReference, Box.transform.position);
                _scoreService.Add(Box.Profile.Points);
                _soundService.Play(Box.Profile.BreakSoundFXPoolReference, Box.transform.position);
                Box.StartCoroutine(DestroyBox());
            }
        }
        #endregion
    }
}