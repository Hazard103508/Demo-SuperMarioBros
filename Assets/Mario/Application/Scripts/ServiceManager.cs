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
            RegisterServicer<IGameDataService>();
            RegisterServicer<ICoinService>();
            RegisterServicer<IScoreService>();
            RegisterServicer<ITimeService>();
            RegisterServicer<IPlayerService>();
            RegisterServicer<ISceneService>();
            RegisterServicer<IMusicService>();
            RegisterServicer<IPoolService>();

            AllServices.Load();
            AllServices.SceneService.LoadMainScene();
        }
        private void OnApplicationQuit()
        {
            UnregisterService<IGameDataService>();
            UnregisterService<ICoinService>();
            UnregisterService<IScoreService>();
            UnregisterService<ITimeService>();
            UnregisterService<IPlayerService>();
            UnregisterService<ISceneService>();
            UnregisterService<IMusicService>();
            UnregisterService<IPoolService>();

            Destroy(gameObject);
        }

        private void RegisterServicer<T>() where T : IGameService => ServiceLocator.Current.Register<T>(transform.GetComponentInChildren<T>());
        private void UnregisterService<T>() where T : IGameService => ServiceLocator.Current.Unregister<T>();
    }
}