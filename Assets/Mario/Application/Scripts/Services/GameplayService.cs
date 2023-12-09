using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using UnityEngine;
using static Mario.Application.Services.LevelService;

namespace Mario.Application.Services
{
    public class GameplayService : MonoBehaviour, IGameplayService
    {
        #region Object
        private ITimeService _timeService;
        private ISoundService _soundService;
        private ILevelService _levelService;
        private IPlayerService _playerService;
        private ISceneService _sceneService;
        private IScoreService _scoreService;

        private readonly int _hurryTime = 100;
        private readonly int _pointsPerSecond = 50;
        private bool _isFlagReached;
        private bool _isHurry;
        private MapConnection _mapConnection;
        private Coroutine _starCO;

        [SerializeField] private PooledSoundProfile _scoreSoundPoolReference;
        #endregion

        #region Events
        public event Action GameFrozen;
        public event Action GameUnfrozen;
        #endregion

        #region Properties
        public bool IsFrozen { get; private set; }
        public bool IsStarman { get; private set; }
        public GameState State { get; set; }
        #endregion

        #region Public Methods
        public void Initalize()
        {
            Screen.SetResolution(1200, 1050, true);

            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();

            _levelService.StartLoading += OnStartLoading;
            _levelService.LoadCompleted += OnLoadCompleted;
            _timeService.TimeOut += OnTimeOut;
            _timeService.TimeChangeded += OnTimeChangeded;
            _playerService.LivesRemoved += OnLivesRemoved;
        }
        public void Dispose()
        {
            _levelService.StartLoading -= OnStartLoading;
            _levelService.LoadCompleted -= OnLoadCompleted;
            _timeService.TimeOut -= OnTimeOut;
            _timeService.TimeChangeded -= OnTimeChangeded;
            _playerService.LivesRemoved -= OnLivesRemoved;
        }
        public void SetNextMap(MapConnection connection)
        {
            _mapConnection = connection;
            _levelService.SetMap(connection.MapProfile);
        }
        public void SetCheckPoint(MapProfile mapProfile) => _levelService.SetMap(mapProfile);
        public void FreezeGame()
        {
            IsFrozen = true;
            _timeService.FreezeTimer();
            GameFrozen?.Invoke();
        }
        public void UnfreezeGame()
        {
            _timeService.UnfreezeTimer();
            GameUnfrozen?.Invoke();
            IsFrozen = false;
        }
        public void SetFlagReached()
        {
            _isFlagReached = true;
            if (_isFlagReached)
                _soundService.PlayTheme(_levelService.MapProfile.Music.VictoryTheme);
        }
        public void ActivateStarman(Action callback) => _starCO = StartCoroutine(ActivateStarmanCO(callback));
        public void SetHouseReached()
        {
            State = GameState.Win;
            StartCoroutine(AddTimeScore());
            StartCoroutine(FinishLevel());
        }
        #endregion

        #region Private Methods
        private void PlayInitTheme()
        {
            if (_timeService.Time > 0 && _timeService.Time <= _hurryTime)
            {
                _isHurry = true;
                _soundService.PlayTheme(_levelService.MapProfile.Music.HurryTheme.Profile, _levelService.MapProfile.Music.HurryTheme.StartTimeContinued);
            }
            else
                _soundService.PlayTheme(_levelService.MapProfile.Music.MainTheme);
        }
        private IEnumerator ReloadAfterDead()
        {
            yield return new WaitForSeconds(3.5f);

            if (_playerService.Lives <= 0)
                _sceneService.LoadGameOverScene();
            else if (_timeService.Time == 0)
                _sceneService.LoadTimeUpScene();
            else
                _levelService.LoadLevel(true);
        }
        private IEnumerator PlayHurryUpTheme()
        {
            _soundService.PlayTheme(_levelService.MapProfile.Music.HurryFX);
            yield return new WaitForSeconds(3.5f);

            if (IsStarman)
                _soundService.PlayTheme(_levelService.MapProfile.Music.StarmanHurry);
            else
                _soundService.PlayTheme(_levelService.MapProfile.Music.HurryTheme.Profile, _levelService.MapProfile.Music.HurryTheme.StartTimeInit);
        }
        private IEnumerator StartGame()
        {
            if (_mapConnection == null || _timeService.Time == 0)
            {
                _timeService.StartTime = _levelService.MapProfile.StartTime;
                _timeService.ResetTimer();
            }

            var camera = Camera.main;
            var virtualCamera = camera.transform.Find("Virtual Camera");
            virtualCamera.gameObject.SetActive(false);
            float cameraXPosition = (_mapConnection != null ? _mapConnection.CameraPositionX : 0) + 8;
            camera.transform.position = new Vector3(cameraXPosition, camera.transform.position.y, camera.transform.position.z);

            _playerService.EnablePlayerCollision(true);
            _playerService.SetPlayerPosition(_mapConnection != null ? _mapConnection.StartPosition : _levelService.MapProfile.StartPosition);
            yield return ShowCustomIntroPosition();
            virtualCamera.gameObject.SetActive(true);

            _playerService.EnableInputs(true);
            _playerService.EnableAutoWalk(_levelService.MapProfile.Autowalk);
            _playerService.ResetState();

            _timeService.StartTimer();
            _mapConnection = null;

            UnfreezeGame();
            PlayInitTheme();
            State = GameState.Play;
        }
        private IEnumerator ShowCustomIntroPosition()
        {
            _playerService.SetActivePlayer(true);
            if (_mapConnection != null)
            {
                if (_mapConnection.StartMode == PlayerStartMode.PipeUp)
                {
                    yield return new WaitForSeconds(1);
                    float _totalTranslate = 0;
                    while (_totalTranslate < 2)
                    {
                        var _posToMove = 2f * Time.deltaTime * Vector3.up;
                        _totalTranslate += _posToMove.y;
                        _playerService.TranslatePlayerPosition(_posToMove);
                        yield return null;
                    }
                }
            }
        }
        private IEnumerator AddTimeScore()
        {
            _timeService.TimeSpeed = 150f;
            _timeService.UnfreezeTimer();

            _soundService.Play(_scoreSoundPoolReference);
            int _previousTime = _timeService.Time;
            while (_timeService.Time > 0)
            {
                int timedif = _previousTime - _timeService.Time;
                int score = timedif * _pointsPerSecond;
                _scoreService.Add(score);
                _previousTime = _timeService.Time;
                yield return null;
            }
            _soundService.Stop();
        }
        private IEnumerator FinishLevel()
        {
            yield return new WaitForSeconds(6);
            _levelService.LoadLevel(true);
        }
        private IEnumerator ActivateStarmanCO(Action callback)
        {
            IsStarman = true;
            if (_isHurry)
                _soundService.PlayTheme(_levelService.MapProfile.Music.StarmanHurry);
            else
                _soundService.PlayTheme(_levelService.MapProfile.Music.Starman);

            yield return new WaitForSeconds(10);
            if (State == GameState.Play)
            {
                IsStarman = false;
                callback.Invoke();
                PlayInitTheme();
            }
        }
        #endregion

        #region Service Methods
        private void OnStartLoading(StartLoadingEvent arg)
        {
            State = GameState.StandBy;
            _isFlagReached = false;
            _isHurry = false;
            IsStarman = false;
            _playerService.SetActivePlayer(false);
            _playerService.SetPlayerPosition(Vector3.zero);

            if (_starCO != null)
                StopCoroutine(_starCO);
        }
        private void OnLoadCompleted() => StartCoroutine(StartGame());
        private void OnTimeChangeded()
        {
            if (_levelService.MapProfile.StartTime <= 0)
                return;

            if (!_isHurry && _timeService.Time <= _hurryTime && !_isFlagReached)
            {
                _isHurry = true;
                StartCoroutine(PlayHurryUpTheme());
            }
        }
        private void OnTimeOut()
        {
            if (!_isFlagReached)
                _playerService.KillPlayerByTimeOut();
        }
        private void OnLivesRemoved() => StartCoroutine(ReloadAfterDead());
        #endregion

        #region Structures
        public enum GameState
        {
            StandBy,
            Play,
            Pause,
            Lose,
            Win
        }
        #endregion
    }
}