using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Items
{
    public class House : MonoBehaviour, IHitableByPlayerFromLeft
    {
        #region Private Methods
        private IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);
            AllServices.SceneService.LoadStandByScene();
        }
        #endregion

        #region On Player Hit
        public void OnHitableByPlayerFromLeft(PlayerController player)
        {
            player.gameObject.SetActive(false);

            AllServices.PlayerService.CanMove = false;
            AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.NextMap;
            AllServices.TimeService.StartTimer();

            StartCoroutine(CloseMap());
        }
        #endregion
    }
}