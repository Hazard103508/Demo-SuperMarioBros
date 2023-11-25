using Mario.Application.Interfaces;
using System.Collections;
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
        public void Dispose()
        {
        }

        public void LoadGameScene() => StartCoroutine(LoadMapSceneCO());
        public void LoadMainScene() => SceneManager.LoadScene("Main");
        public void LoadGameOverScene() => SceneManager.LoadScene("GameOver");
        public void LoadTimeUpScene() => SceneManager.LoadScene("TimeUp");
        #endregion

        #region Private Methods
        private IEnumerator LoadMapSceneCO()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
            yield return new WaitUntil(() => asyncOperation.isDone && _levelService.IsLoadCompleted);
        }
        #endregion
    }
}