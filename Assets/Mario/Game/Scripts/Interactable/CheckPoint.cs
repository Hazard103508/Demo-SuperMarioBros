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
            if (Services.GameDataService.CurrentMapProfile.CheckPoints.Length > _checkPointIndex)
                Services.GameDataService.NextMapProfile = Services.GameDataService.CurrentMapProfile.CheckPoints[_checkPointIndex];

            base.OnHitCheckPoint(player);
        }
    }
}