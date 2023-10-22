using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Items.Mushroom;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.RedMushroom
{
    public class MushroomRedStateWalk : MushroomStateWalk
    {
        #region Objects
        private readonly IScoreService _scoreService;
        #endregion

        #region Properties
        private new MushroomRed Mushroom => (MushroomRed)base.Mushroom;
        #endregion

        #region Constructor
        public MushroomRedStateWalk(MushroomRed mushroom) : base(mushroom)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
        }
        #endregion

        #region Private Methods
        protected override bool CollectMushroom(PlayerController player)
        {
            if (base.CollectMushroom(player))
            {
                _scoreService.Add(Mushroom.Profile.Points);
                _scoreService.ShowPoints(Mushroom.Profile.Points, Mushroom.transform.position + Vector3.up * 1.75f, 0.8f, 3f);
                player.Buff();
                return true;
            }
            return false;
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => CollectMushroom(player);
        #endregion
    }
}