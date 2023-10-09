using Mario.Application.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Application.Services
{
    public class SceneService : MonoBehaviour, ISceneService
    {
        #region Objects
        private ILevelService _levelService;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }

        public void LoadMapScene(float minDelay) => StartCoroutine(LoadMapSceneCO(minDelay));
        public void LoadMainScene() => SceneManager.LoadScene("Main");
        public void LoadStandByScene() => SceneManager.LoadScene("StandBy");
        public void LoadGameOverScene() => SceneManager.LoadScene("GameOver");
        public void LoadTimeUpScene() => SceneManager.LoadScene("TimeUp");
        #endregion

        #region Private Methods
        private IEnumerator LoadMapSceneCO(float delay)
        {
            yield return new WaitForSeconds(delay);

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
            yield return new WaitUntil(() => asyncOperation.isDone && _levelService.IsLoadCompleted);
        }
        #endregion
    }
}