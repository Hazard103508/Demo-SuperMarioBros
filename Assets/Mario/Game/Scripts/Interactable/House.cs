using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class House : MonoBehaviour, ILeftHitable
    {
        public void OnHitFromLeft(PlayerController player)
        {
            player.gameObject.SetActive(false);

            AllServices.PlayerService.CanMove = false;
            AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.NextMap;
            AllServices.TimeService.StartTimer();

            StartCoroutine(CloseMap());
        }

        public IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);
            AllServices.SceneService.LoadStandByScene();
        }
    }
}