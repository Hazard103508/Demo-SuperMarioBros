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
        private bool isCollected;
        #endregion

        #region Private Methods
        protected override void CollectMushroom(PlayerController player)
        {
            if (isCollected || IsRising)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_redMushroomProfile.Points);
            AllServices.ScoreService.ShowPoint(_redMushroomProfile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion
    }
}