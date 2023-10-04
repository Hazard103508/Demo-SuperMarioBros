using Mario.Application.Interfaces;
using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.Main
{
    public class GameOverScene : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake()
        {
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
        }
        private void Start()
        {
            StartCoroutine(LoadMainScene());
        }

        private IEnumerator LoadMainScene()
        {
            yield return new WaitForSeconds(7);
            _sceneService.LoadMainScene();
        }
    }
}