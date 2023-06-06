using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : MonoBehaviour, ILeftHitable, IRightHitable, IBottomHitable, ITopHitable
    {
        [SerializeField] private int _checkPointIndex;

        public void OnHitFromLeft(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromRight(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromBottom(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromTop(PlayerController player) => OnHitCheckPoint(player);

        private void OnHitCheckPoint(PlayerController player)
        {
            if (AllServices.GameDataService.CurrentMapProfile.CheckPoints.Length > _checkPointIndex)
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.CheckPoints[_checkPointIndex];

            Destroy(gameObject);
        }
    }
}