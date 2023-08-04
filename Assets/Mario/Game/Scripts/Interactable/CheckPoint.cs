using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : TriggerPoint
    {
        [SerializeField] private int _checkPointIndex;

        protected override void OnHitCheckPoint(PlayerController player)
        {
            if (AllServices.GameDataService.CurrentMapProfile.CheckPoints.Length > _checkPointIndex)
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.CheckPoints[_checkPointIndex];

            base.OnHitCheckPoint(player);
        }
    }
}