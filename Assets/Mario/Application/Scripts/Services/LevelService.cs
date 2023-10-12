using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.Maps;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        private IAddressablesService _addressablesService;
        private IPoolService _poolService;
        private ISceneService _sceneService;
        private IPlayerService _playerService;
        private ITimeService _timeService;
        private ISoundService _soundService;

        private readonly int _hurryTime = 100;

        [SerializeField] private MapProfile _currentMapProfile;
        private AddressablesLoaderContainer _assetLoaderContainer;
        private bool _isGoalReached;
        private bool _isHurry;
        private Coroutine _hurryCO;
        #endregion

        #region Properties
        public MapProfile CurrentMapProfile { get; set; }
        public MapProfile NextMapProfile { get; set; }
        public bool IsGoalReached
        {
            get => _isGoalReached;
            set
            {
                _isGoalReached = value;
                if (value)
                    GoalReached?.Invoke();
            }
        }
        public bool IsLoadCompleted { get; private set; }
        #endregion

        #region Events
        public event Action LevelLoaded;
        public event Action GoalReached;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();

            CurrentMapProfile = _currentMapProfile;
            _assetLoaderContainer = new AddressablesLoaderContainer();
            _playerService.LivesRemoved += OnLivesRemoved;
            _timeService.TimeOut += OnTimeOut;
            _timeService.TimeChangeded += OnTimeChangeded;
        }
        public void Dispose()
        {
            _timeService.TimeChangeded -= OnTimeChangeded;
            _timeService.TimeOut -= OnTimeOut;
            _playerService.LivesRemoved -= OnLivesRemoved;
        }

        public async void LoadLevel(Transform parent)
        {
            IsGoalReached = false;
            Camera.main.backgroundColor = CurrentMapProfile.MapInit.BackgroundColor;

            LoadAsyncReferences();
            await LoadMapSections(parent);
            StartCoroutine(StartGame());
        }
        public void UnloadLevel()
        {
            SetNextMap();
            _assetLoaderContainer.Clear();
            _poolService.ClearPool();

            if (_hurryCO != null)
                StopCoroutine(_hurryCO);
        }
        #endregion

        #region Private Methods
        private void LoadAsyncReferences()
        {
            foreach (PooledProfileGroup poolGroup in CurrentMapProfile.PoolProfiles)
            {
                _assetLoaderContainer.Register<PooledObjectProfile, GameObject>(poolGroup.PooledObjectProfiles);
                _assetLoaderContainer.Register<PooledSoundProfile, AudioClip>(poolGroup.PooledSoundProfiles);
                _assetLoaderContainer.Register<PooledUIProfile, GameObject>(poolGroup.PooledUIProfiles);
            }

            _assetLoaderContainer.LoadAssetAsync<GameObject>();
            _assetLoaderContainer.LoadAssetAsync<AudioClip>();
        }
        private void SetNextMap()
        {
            if (NextMapProfile != null)
            {
                _timeService.StartTime =
                    NextMapProfile.Time.Type == MapTimeType.Continuated ? _timeService.Time :
                    NextMapProfile.Time.Type == MapTimeType.Beginning ? NextMapProfile.Time.StartTime :
                    0;

                CurrentMapProfile = NextMapProfile;
                NextMapProfile = null;
            }
        }
        private async Task LoadMapSections(Transform parent)
        {
            int positionX = 0;
            foreach (var references in CurrentMapProfile.MapSectionReferences)
            {
                await _addressablesService.LoadAssetAsync<GameObject>(references);
                var mapSection = _addressablesService.GetAssetReference<GameObject>(references);
                LoadMapSection(mapSection, ref positionX, parent);
            }
            CurrentMapProfile.Width = positionX;
        }
        private void LoadMapSection(GameObject mapSectionReference, ref int positionX, Transform parent)
        {
            var mapObj = Instantiate(mapSectionReference, parent);
            mapObj.transform.position = Vector3.right * positionX;

            var mapSection = mapObj.GetComponent<MapSection>();

            var unloader = mapObj.AddComponent<MapSectionUnloader>();
            unloader.Width = mapSection.Size.Width;

            positionX += mapSection.Size.Width;
        }
        private IEnumerator StartGame()
        {
            yield return new WaitUntil(() => _assetLoaderContainer.IsLoadCompleted);
            yield return new WaitForSeconds(CurrentMapProfile.MapInit.BlackScreenTime);

            _timeService.StartTime = CurrentMapProfile.Time.StartTime;
            _timeService.ResetTimer();

            IsLoadCompleted = true;
            _isHurry = false;
            _playerService.SetPlayerPosition(CurrentMapProfile.MapInit.StartPosition);
            _playerService.SetPlayerEnabled(true);

            LevelLoaded.Invoke();

            yield return ShowCustomIntroPosition();
            _timeService.StartTimer();
            _playerService.SetPlayerMovable(true);
            PlayInitTheme();
        }
        private IEnumerator ShowCustomIntroPosition()
        {
            if (CurrentMapProfile.MapInit.StartLocation == PlayerStartLocation.PipeUp)
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
            _soundService.PlayTheme(CurrentMapProfile.Music.HurryFX);
            yield return new WaitForSeconds(3.5f);

            _soundService.PlayTheme(CurrentMapProfile.Music.HurryTheme.Profile, CurrentMapProfile.Music.HurryTheme.StartTimeInit);
        }
        private void PlayInitTheme()
        {
            if (_timeService.Time <= _hurryTime)
            {
                _isHurry = true;
                _soundService.PlayTheme(CurrentMapProfile.Music.HurryTheme.Profile, CurrentMapProfile.Music.HurryTheme.StartTimeContinued);
            }
            else
                _soundService.PlayTheme(CurrentMapProfile.Music.MainTheme);
        }
        #endregion

        #region Service Methods
        private void OnLivesRemoved()
        {
            _timeService.FreezeTimer();
            StartCoroutine(ReloadAfterDead());
        }
        private void OnTimeChangeded()
        {
            if (CurrentMapProfile.Time.Type == MapTimeType.None)
                return;

            if (!_isHurry && _timeService.Time <= _hurryTime && !IsGoalReached)
            {
                _isHurry = true;
                _hurryCO = StartCoroutine(PlayHurryUpTheme());
            }
        }
        private void OnTimeOut()
        {
            _playerService.KillPlayerByTimeOut();
        }
        #endregion
    }
}