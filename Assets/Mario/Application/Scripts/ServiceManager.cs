using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Application
{
    public class ServiceManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void Start()
        {
            ServiceLocator.Initiailze();
            RegisterServicer<IAddressablesService>();
            RegisterServicer<IPoolService>();
            RegisterServicer<IGameDataService>();
            RegisterServicer<ICoinService>();
            RegisterServicer<IScoreService>();
            RegisterServicer<ITimeService>();
            RegisterServicer<IPlayerService>();
            RegisterServicer<ISceneService>();
            RegisterServicer<IMusicService>();

            Services.Services.Load();
            Services.Services.SceneService.LoadMainScene();
        }
        private void OnApplicationQuit()
        {
            UnregisterService<IAddressablesService>();
            UnregisterService<IPoolService>();
            UnregisterService<IGameDataService>();
            UnregisterService<ICoinService>();
            UnregisterService<IScoreService>();
            UnregisterService<ITimeService>();
            UnregisterService<IPlayerService>();
            UnregisterService<ISceneService>();
            UnregisterService<IMusicService>();

            Destroy(gameObject);
        }

        private void RegisterServicer<T>() where T : IGameService => ServiceLocator.Current.Register<T>(transform.GetComponentInChildren<T>());
        private void UnregisterService<T>() where T : IGameService => ServiceLocator.Current.Unregister<T>();
    }
}