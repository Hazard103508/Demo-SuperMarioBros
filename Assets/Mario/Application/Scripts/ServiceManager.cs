using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            ServiceLocator.Current.Register<IGameDataService>(GetComponent<GameDataService>());
            ServiceLocator.Current.Register<ICoinService>(GetComponent<CoinService>());
            ServiceLocator.Current.Register<IScoreService>(GetComponent<ScoreService>());
            ServiceLocator.Current.Register<ITimeService>(GetComponent<TimeService>());
            ServiceLocator.Current.Register<IPlayerService>(GetComponent<PlayerService>());
            ServiceLocator.Current.Register<ISceneService>(GetComponent<SceneService>());

            AllServices.Load();
            SceneManager.LoadScene("StandBy", LoadSceneMode.Single);
        }
        private void OnApplicationQuit()
        {
            ServiceLocator.Current.Unregister<IGameDataService>();
            ServiceLocator.Current.Unregister<ICoinService>();
            ServiceLocator.Current.Unregister<IScoreService>();
            ServiceLocator.Current.Unregister<ITimeService>();
            ServiceLocator.Current.Unregister<IPlayerService>();
            ServiceLocator.Current.Unregister<ISceneService>();

            Destroy(gameObject);
        }
    }
}