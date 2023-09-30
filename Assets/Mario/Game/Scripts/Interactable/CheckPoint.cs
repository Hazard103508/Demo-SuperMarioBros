using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : TriggerPoint
    {
        #region Objects
        [SerializeField] private int _checkPointIndex;
        #endregion

        #region Protected Methods
        protected override void OnHitCheckPoint(PlayerController player)
        {
            if (Services.GameDataService.CurrentMapProfile.CheckPoints.Length > _checkPointIndex)
                Services.GameDataService.NextMapProfile = Services.GameDataService.CurrentMapProfile.CheckPoints[_checkPointIndex];

            base.OnHitCheckPoint(player);
        }
        #endregion
    }
}