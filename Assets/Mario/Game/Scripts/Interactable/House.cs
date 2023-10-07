using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class House : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
        private ILevelService _levelService;
        private ISceneService _sceneService;
        private ITimeService _timeService;
        #endregion

        #region Private Methods
        private void Awake()
        {
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }
        private IEnumerator CloseMap()
        {
            yield return new WaitForSeconds(6);
            _sceneService.LoadStandByScene();
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            player.gameObject.SetActive(false);

            _levelService.NextMapProfile = _levelService.CurrentMapProfile.NextMap;
            _timeService.StartTimer();

            StartCoroutine(CloseMap());
        }
        #endregion
    }
}