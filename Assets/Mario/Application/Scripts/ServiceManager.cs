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
            RegisterServicer<IPauseService>();
            RegisterServicer<IInputService>();
            RegisterServicer<IGameplayService>();

            ServiceLocator.Current.Initalize();
            ServiceLocator.Current.Get<ISceneService>().LoadMainScene();
        }
        private void OnApplicationQuit()
        {
            ServiceLocator.Current.Dispose();
            Destroy(gameObject);
        }

        private void RegisterServicer<T>() where T : IGameService => ServiceLocator.Current.Register(GetComponentInChildren<T>());
    }
}