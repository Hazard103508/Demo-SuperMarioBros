using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        #region Objects
        [SerializeField] private RedMushroomProfile _redMushroomProfile;
        #endregion

        #region Private Methods
        protected override void CollectMushroom(PlayerController player)
        {
            gameObject.layer = 0;
            Services.ScoreService.Add(_redMushroomProfile.Points);
            Services.ScoreService.ShowPoints(_redMushroomProfile.Points, transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion
    }
}