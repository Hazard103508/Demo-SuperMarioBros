using Mario.Application.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Mario.Application.Services
{
    public class SceneService : MonoBehaviour, ISceneService
    {
        public void LoadService()
        {
        }

        public void LoadMapScene(float minDelay) => StartCoroutine(LoadMapSceneCO(minDelay));
        public void LoadMainScene() => SceneManager.LoadScene("Main");
        public void LoadStandByScene() => SceneManager.LoadScene("StandBy");
        public void LoadGameOverScene() => SceneManager.LoadScene("GameOver");
        public void LoadTimeUpScene() => SceneManager.LoadScene("TimeUp");

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
    }
}