using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.Main
{
    public class GameOverScene : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(LoadMainScene());
        }

        private IEnumerator LoadMainScene()
        {
            yield return new WaitForSeconds(7);
            AllServices.SceneService.LoadMainScene();
        }
    }
}