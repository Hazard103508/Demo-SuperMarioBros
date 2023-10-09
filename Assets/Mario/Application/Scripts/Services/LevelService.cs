using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.Maps;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Player;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

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

        [SerializeField] private MapProfile _currentMapProfile;
        private AddressablesLoaderContainer _assetLoaderContainer;
        private bool _isGoalReached;
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
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();

            CurrentMapProfile = _currentMapProfile;
            _assetLoaderContainer = new AddressablesLoaderContainer();
        }

        public async void LoadLevel(Transform parent)
        {
            IsGoalReached = false;
            Camera.main.backgroundColor = CurrentMapProfile.MapInit.BackgroundColor;
            _playerService.LivesRemoved += OnLivesRemoved;
            _timeService.ResetTimer();

            LoadAssetsReferences();
            await LoadMapSections(parent);
            StartCoroutine(StartGame());
        }
        public void UnloadLevel()
        {
            SetNextMap();
            _assetLoaderContainer.Clear();
            _poolService.ClearPool();
            _playerService.LivesRemoved -= OnLivesRemoved;
        }
        #endregion

        #region Private Methods
        private void LoadAssetsReferences()
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
            yield return SetPlayerInitPosition();
            //yield return StartGameFalling();
            yield return new WaitUntil(() => _assetLoaderContainer.IsLoadCompleted);
            yield return new WaitForSeconds(CurrentMapProfile.MapInit.BlackScreenTime);

            _timeService.StartTimer();
            _playerService.PlayerController.gameObject.SetActive(true);
            IsLoadCompleted = true;
            LevelLoaded.Invoke();
        }
        private IEnumerator SetPlayerInitPosition()
        {
            if (CurrentMapProfile.MapInit.StartLocation == PlayerStartLocation.Pipe)
            {
                yield return new WaitForSeconds(1);

                while (_playerService.PlayerController.transform.position.y < CurrentMapProfile.MapInit.StartPosition.y + 2)
                {
                    _playerService.PlayerController.transform.Translate(2f * Time.deltaTime * Vector3.up);
                    yield return null;
                }
            }
            else
                _playerService.PlayerController.transform.position = CurrentMapProfile.MapInit.StartPosition.y * Vector3.up;
        }
        //private IEnumerator StartGameFalling()
        //{
        //    if (CurrentMapProfile.MapInit.StartLocation == PlayerStartLocation.Falling)
        //    {
        //        //_player.InputActions.Move.x = 1; // TODO
        //        yield return new WaitForEndOfFrame();
        //    }
        //}
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
        private void OnLivesRemoved()
        {
            _timeService.StopTimer();
            StartCoroutine(ReloadAfterDead());
        }
        #endregion
    }
}