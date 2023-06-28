using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Application
{
    public class ServiceManager : MonoBehaviour
    {
        [SerializeField] private GameDataService _gameDataService;
        [SerializeField] private CoinService _coinService;
        [SerializeField] private ScoreService _scoreService;
        [SerializeField] private TimeService _timeService;
        [SerializeField] private PlayerService _playerService;
        [SerializeField] private SceneService _sceneService;
        [SerializeField] private MusicService _musicService;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
        private void Start()
        {
            ServiceLocator.Initiailze();
            ServiceLocator.Current.Register<IGameDataService>(_gameDataService);
            ServiceLocator.Current.Register<ICoinService>(_coinService);
            ServiceLocator.Current.Register<IScoreService>(_scoreService);
            ServiceLocator.Current.Register<ITimeService>(_timeService);
            ServiceLocator.Current.Register<IPlayerService>(_playerService);
            ServiceLocator.Current.Register<ISceneService>(_sceneService);
            ServiceLocator.Current.Register<IMusicService>(_musicService);

            AllServices.Load();
            AllServices.SceneService.LoadStandByScene();
        }
        private void OnApplicationQuit()
        {
            ServiceLocator.Current.Unregister<IGameDataService>();
            ServiceLocator.Current.Unregister<ICoinService>();
            ServiceLocator.Current.Unregister<IScoreService>();
            ServiceLocator.Current.Unregister<ITimeService>();
            ServiceLocator.Current.Unregister<IPlayerService>();
            ServiceLocator.Current.Unregister<ISceneService>();
            ServiceLocator.Current.Unregister<IMusicService>();

            Destroy(gameObject);
        }
    }
}