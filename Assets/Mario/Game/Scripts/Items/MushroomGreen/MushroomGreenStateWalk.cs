using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Items.Mushroom;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.GreenMushroom
{
    public class MushroomGreenStateWalk : MushroomStateWalk
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly IPlayerService _playerService;
        #endregion

        #region Properties
        private new MushroomGreen Mushroom => (MushroomGreen)base.Mushroom;
        #endregion

        #region Constructor
        public MushroomGreenStateWalk(MushroomGreen mushroom) : base(mushroom)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        #endregion

        #region Private Methods
        public void CollectMushroom()
        {
            if (!Mushroom.gameObject.activeSelf)
                return;

            Mushroom.gameObject.layer = 0;
            _playerService.AddLife();
            _scoreService.Show1UP(Mushroom.transform.position + Vector3.up * 1.70f, 0.8f, 3f);

            Mushroom.gameObject.SetActive(false);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => CollectMushroom();
        public override void OnHittedByPlayerFromBottom(PlayerController player) => CollectMushroom();
        public override void OnHittedByPlayerFromLeft(PlayerController player) => CollectMushroom();
        public override void OnHittedByPlayerFromRight(PlayerController player) => CollectMushroom();
        #endregion
    }
}