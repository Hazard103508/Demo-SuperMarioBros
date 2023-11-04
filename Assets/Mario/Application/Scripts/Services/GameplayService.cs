using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.ScriptableObjects.Map;
using System;
using System.Collections;
using UnityEngine;

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
        #endregion

        #region Events
        public event Action GameFreezed;
        public event Action GameUnfreezed;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _scoreService = ServiceLocator.Current.Get<IScoreService>();

            _levelService.LoadCompleted += OnLoadCompleted;
            _levelService.LoadingConnection += OnLoadingConnection;
            _timeService.TimeOut += OnTimeOut;
            _timeService.TimeChangeded += OnTimeChangeded;
            _playerService.LivesRemoved += OnLivesRemoved;
        }
        public void Dispose()
        {
            _levelService.LoadCompleted -= OnLoadCompleted;
            _levelService.LoadingConnection -= OnLoadingConnection;
            _timeService.TimeOut -= OnTimeOut;
            _timeService.TimeChangeded -= OnTimeChangeded;
            _playerService.LivesRemoved -= OnLivesRemoved;
        }
        public void SetNextMap(MapConnection connection) => _mapConnection = connection;
        public void FreezeGame()
        {
            _timeService.FreezeTimer();
            GameFreezed?.Invoke();
        }
        public void UnfreezeGame()
        {
            _timeService.UnfreezeTimer();
            GameUnfreezed?.Invoke();
        }
        public void SetFlagReached()
        {
            _isFlagReached = true;
            if (_isFlagReached)
                _soundService.PlayTheme(_levelService.MapProfile.Music.VictoryTheme);
        }
        public void SetHouseReached()
        {
            StartCoroutine(AddTimeScore());
            StartCoroutine(FinishLevel());
        }
        #endregion

        #region Private Methods
        private void PlayInitTheme()
        {
            if (_timeService.Time <= _hurryTime)
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
                _sceneService.LoadStandByScene();
        }
        private IEnumerator PlayHurryUpTheme()
        {
            _soundService.PlayTheme(_levelService.MapProfile.Music.HurryFX);
            yield return new WaitForSeconds(3.5f);

            _soundService.PlayTheme(_levelService.MapProfile.Music.HurryTheme.Profile, _levelService.MapProfile.Music.HurryTheme.StartTimeInit);
        }
        private IEnumerator StartGame()
        {
            _isFlagReached = false;
            _isHurry = false;

            if (_mapConnection == null)
            {
                _timeService.StartTime = _levelService.MapProfile.StartTime;
                _timeService.ResetTimer();
            }

            _playerService.SetPlayerPosition(_mapConnection != null ? _mapConnection.StartPosition : _levelService.MapProfile.StartPosition);

            yield return ShowCustomIntroPosition();

            _timeService.StartTimer();
            _playerService.EnableInputs(true);
            _mapConnection = null;

            UnfreezeGame();
            PlayInitTheme();
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

            int _previousTime = _timeService.Time;
            while (_timeService.Time > 0)
            {
                int timedif = _previousTime - _timeService.Time;
                int score = timedif * _pointsPerSecond;
                _scoreService.Add(score);
                _previousTime = _timeService.Time;
                yield return null;
            }
        }
        private IEnumerator FinishLevel()
        {
            yield return new WaitForSeconds(6);
            _levelService.LoadNextLevel();
        }
        #endregion

        #region Service Methods
        private void OnLoadCompleted() => StartCoroutine(StartGame());
        private MapProfile OnLoadingConnection() => _mapConnection != null ? _mapConnection.MapProfile : null;
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
        private void OnTimeOut() => _playerService.KillPlayerByTimeOut();
        private void OnLivesRemoved() => StartCoroutine(ReloadAfterDead());
        #endregion
    }
}