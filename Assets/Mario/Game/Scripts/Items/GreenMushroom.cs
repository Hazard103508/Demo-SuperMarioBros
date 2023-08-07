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

        #region Protected Methods
        protected override void OnPoolObjectReseted()
        {
            isCollected = false;
            base.OnPoolObjectReseted();
        }
        #endregion

        #region Private Methods
        protected override void CollectMushroom(PlayerController player)
        {
            if (isCollected || IsRising)
                return;

            isCollected = true;
            Services.PlayerService.AddLife();
            Services.ScoreService.Show1UP(transform.position + Vector3.up * 1.70f, 0.8f, 3f);

            gameObject.SetActive(false);
        }
        #endregion
    }
}