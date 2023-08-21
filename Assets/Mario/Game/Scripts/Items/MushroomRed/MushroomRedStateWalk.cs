using Mario.Application.Services;
using Mario.Game.Items.Mushroom;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items.RedMushroom
{
    public class MushroomRedStateWalk : MushroomStateWalk
    {
        #region Properties
        private new MushroomRed Mushroom => (MushroomRed)base.Mushroom;
        #endregion

        #region Constructor
        public MushroomRedStateWalk(MushroomRed mushroom) : base(mushroom)
        {
        }
        #endregion

        #region Private Methods
        public void CollectMushroom(PlayerController_OLD player)
        {
            if (!Mushroom.gameObject.activeSelf)
                return;

            Mushroom.gameObject.layer = 0;
            Services.ScoreService.Add(Mushroom.Profile.Points);
            Services.ScoreService.ShowPoints(Mushroom.Profile.Points, Mushroom.transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            Mushroom.gameObject.SetActive(false);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController_OLD player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromLeft(PlayerController_OLD player) => CollectMushroom(player);
        public override void OnHittedByPlayerFromRight(PlayerController_OLD player) => CollectMushroom(player);
        #endregion
    }
}