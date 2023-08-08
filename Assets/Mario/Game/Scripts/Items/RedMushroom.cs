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
            Services.ScoreService.Add(_redMushroomProfile.Points);
            Services.ScoreService.ShowPoints(_redMushroomProfile.Points, transform.position + Vector3.up * 1.75f, 0.8f, 3f);

            player.Buff();
            gameObject.SetActive(false);
        }
        #endregion
    }
}