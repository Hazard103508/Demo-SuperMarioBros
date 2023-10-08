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

            CurrentMapProfile = _currentMapProfile;
        }
        public async void LoadLevel(Transform parent)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            IsGoalReached = false;
            Camera.main.backgroundColor = CurrentMapProfile.MapInit.BackgroundColor;
            _playerService.LivesRemoved += OnLivesRemoved;
            _timeService.ResetTimer();

            await LoadAssets();
            LoadMapSections(parent);
            StartCoroutine(StartGame(stopwatch));
        }
        public void UnloadLevel()
        {
            SetNextMap();
            _addressablesService.ReleaseAllAssets();
            _poolService.ClearPool();
            _playerService.LivesRemoved -= OnLivesRemoved;
        }
        #endregion

        #region Private Methods
        private async Task LoadAssets()
        {
            foreach (PooledProfileGroup poolGroup in CurrentMapProfile.PoolProfiles)
            {
                await LoadObjectsPool<GameObject, PooledObjectProfile>(poolGroup.PooledObjectProfiles, profile => profile.Reference);
                await LoadObjectsPool<AudioClip, PooledSoundProfile>(poolGroup.PooledSoundProfiles, profile => profile.Reference);
                await LoadObjectsPool<GameObject, PooledUIProfile>(poolGroup.PooledUIProfiles, profile => profile.Reference);
            }
        }
        private async Task LoadObjectsPool<T, R>(R[] poolItems, Func<R, AssetReference> getReferenceFunc) where R : PooledBaseProfile
        {
            foreach (R item in poolItems)
            {
                await _addressablesService.LoadAssetAsync<T>(getReferenceFunc(item));
            }
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
        private void LoadMapSections(Transform parent)
        {
            int positionX = 0;
            foreach (var mapSection in CurrentMapProfile.MapSectionReferences)
                LoadMapSection(mapSection, ref positionX, parent);

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

        private IEnumerator StartGame(Stopwatch stopwatch)
        {
            yield return SetPlayerInitPosition();
            //yield return StartGameFalling();

            _timeService.StartTimer();
            _playerService.PlayerController.gameObject.SetActive(true);

            stopwatch.Stop();
            float loadingTime = (float)TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds;
            if (CurrentMapProfile.MapInit.BlackScreenTime > loadingTime)
                yield return new WaitForSeconds(CurrentMapProfile.MapInit.BlackScreenTime - loadingTime); // fuerza una pantalla negra de demora

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
        private IEnumerator ReloadMap()
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
            StartCoroutine(ReloadMap());
        }
        #endregion
    }
}