using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom.Mushroom
    {
        #region Objects
        new private RedMushroomProfile Profile => (RedMushroomProfile)Profile;
        #endregion

        #region Private Methods
        protected override void CollectMushroom(PlayerController player)
        {
            gameObject.layer = 0;
            Services.ScoreService.Add(Profile.Points);
            Services.ScoreService.ShowPoints(Profile.Points, transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion
    }
}