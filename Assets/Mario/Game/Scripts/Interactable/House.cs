using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class House : MonoBehaviour, IHitableByPlayerFromLeft
    {
        #region Private Methods
        private IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);
            Services.SceneService.LoadStandByScene();
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            player.gameObject.SetActive(false);

            Services.PlayerService.CanMove = false;
            Services.GameDataService.NextMapProfile = Services.GameDataService.CurrentMapProfile.NextMap;
            Services.TimeService.StartTimer();

            StartCoroutine(CloseMap());
        }
        #endregion
    }
}