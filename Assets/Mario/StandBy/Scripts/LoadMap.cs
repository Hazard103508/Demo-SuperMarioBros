using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.StandBy
{
    public class LoadMap : MonoBehaviour
    {
        private ISceneService _sceneService;

        private void Awake()
        {
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
        }
        private void Start()
        {
            _sceneService.LoadMapScene(2.5f);
            //_sceneService.LoadMapScene(0);
        }
    }
}