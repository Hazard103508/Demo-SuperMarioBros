using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mario.StandBy
{
    public class LoadMap : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LosMap());
        }

        private IEnumerator LosMap()
        {
            yield return null;
            float timer = 0;
            float minDelay = 2.5f; // demora forzada para simular el original

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