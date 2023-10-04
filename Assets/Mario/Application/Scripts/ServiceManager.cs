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
            RegisterServicer<ILevelService>();
            RegisterServicer<IPoolService>();
            RegisterServicer<ISoundService>();
            RegisterServicer<ICoinService>();
            RegisterServicer<IScoreService>();
            RegisterServicer<ITimeService>();
            RegisterServicer<IPlayerService>();
            RegisterServicer<ISceneService>();
            RegisterServicer<IThemeMusicService>();

            Services.Services.Load();
            ServiceLocator.Current.Get<ISceneService>().LoadMainScene();
        }
        private void OnApplicationQuit()
        {
            UnregisterService<IAddressablesService>();
            UnregisterService<ILevelService>();
            UnregisterService<IPoolService>();
            UnregisterService<ISoundService>();
            UnregisterService<ICoinService>();
            UnregisterService<IScoreService>();
            UnregisterService<ITimeService>();
            UnregisterService<IPlayerService>();
            UnregisterService<ISceneService>();
            UnregisterService<IThemeMusicService>();

            Destroy(gameObject);
        }

        private void RegisterServicer<T>() where T : IGameService => ServiceLocator.Current.Register<T>(GetComponentInChildren<T>());
        private void UnregisterService<T>() where T : IGameService => ServiceLocator.Current.Unregister<T>();
    }
}