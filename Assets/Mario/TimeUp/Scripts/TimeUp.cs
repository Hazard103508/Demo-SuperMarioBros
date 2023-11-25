using Mario.Application.Interfaces;
using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.TimeUp
{
    public class TimeUp : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake()
        {
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
        }
        private void Start()
        {
            StartCoroutine(LoadStandBy());
        }
        private IEnumerator LoadStandBy()
        {
            yield return new WaitForSeconds(2.4f);
            _sceneService.LoadGameScene();
        }
    }
}