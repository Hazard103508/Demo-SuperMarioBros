using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items
{
    public class GreenMushroom : Mushroom.Mushroom
    {
        #region Private Methods
        public override void CollectMushroom(PlayerController player)
        {
            gameObject.layer = 0;
            Services.PlayerService.AddLife();
            Services.ScoreService.Show1UP(transform.position + Vector3.up * 1.70f, 0.8f, 3f);

            gameObject.SetActive(false);
        }
        #endregion
    }
}