using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items
{
    public class GreenMushroom : Mushroom
    {
        #region Objects
        private bool isCollected;
        #endregion

        #region Protected Methods
        protected override void ResetMushroom()
        {
            isCollected = false;
            base.ResetMushroom();
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