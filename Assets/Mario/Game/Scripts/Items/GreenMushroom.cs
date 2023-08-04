using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class GreenMushroom : Mushroom
    {
        #region Objects
        [SerializeField] private GreenMushroomProfile _greenMushroomProfile;
        private bool isCollected;
        #endregion

        #region Private Methods
        private void CollectMushroom(PlayerController player)
        {
            if (isCollected || IsRising)
                return;

            isCollected = true;
            AllServices.PlayerService.AddLife();
            AllServices.ScoreService.ShowLabel(_greenMushroomProfile.Sprite1UP, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            Destroy(gameObject);
        }
        #endregion

        #region On Player Hit
        public override void OnHitableByPlayerFromLeft(PlayerController player) => CollectMushroom(player);
        public override void OnHitableByPlayerFromBottom(PlayerController player) => CollectMushroom(player);
        public override void OnHitableByPlayerFromRight(PlayerController player) => CollectMushroom(player);
        public override void OnHitableByPlayerFromTop(PlayerController player) => CollectMushroom(player);
        #endregion
    }
}