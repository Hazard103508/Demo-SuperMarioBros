using Mario.Application.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.Application.Services
{
    public class SceneService : MonoBehaviour, ISceneService
    {
        #region Public Methods
        public void Initalize()
        {
        }
        public void LoadMapScene(float minDelay) => StartCoroutine(LoadMapSceneCO(minDelay));
        public void LoadMainScene() => SceneManager.LoadScene("Main");
        public void LoadStandByScene() => SceneManager.LoadScene("StandBy");
        public void LoadGameOverScene() => SceneManager.LoadScene("GameOver");
        public void LoadTimeUpScene() => SceneManager.LoadScene("TimeUp");
        #endregion

        #region Private Methods
        private IEnumerator LoadMapSceneCO(float minDelay)
        {
            yield return null;

            float timer = 0;

            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Game");
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone || timer < minDelay)
            {
                if (asyncOperation.progress >= 0.9f && timer >= minDelay)
                    asyncOperation.allowSceneActivation = true;

                timer += Time.deltaTime;
                yield return null;
            }
        }
        #endregion
    }
}