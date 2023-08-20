using Mario.Application.Services;
using Mario.Game.Items.Mushroom;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.GreenMushroom
{
    public class GreenMushroomStateWalk : MushroomStateWalk
    {
        #region Properties
        private new GreenMushroom Mushroom => (GreenMushroom)base.Mushroom;
        #endregion

        #region Constructor
        public GreenMushroomStateWalk(GreenMushroom mushroom) : base(mushroom)
        {
        }
        #endregion

        #region Private Methods
        public void CollectMushroom()
        {
            if (!Mushroom.gameObject.activeSelf)
                return;

            Mushroom.gameObject.layer = 0;
            Services.PlayerService.AddLife();
            Services.ScoreService.Show1UP(Mushroom.transform.position + Vector3.up * 1.70f, 0.8f, 3f);

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