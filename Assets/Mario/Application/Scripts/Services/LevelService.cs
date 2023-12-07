using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        [SerializeField] private MapProfile[] _worldProfiles;

        private IPoolService _poolService;

        private Dictionary<string, MapProfile> _worldProfilesDic;
        private AddressablesLoaderContainer _assetLoaderContainer;
        private GameObject _root;
        private string _nextMap;
        private float _standbyDelay = 2.5f;
        #endregion

        #region Events
        public event Action<StartLoadingEvent> StartLoading;
        public event Action LoadCompleted;
        #endregion

        #region Properties
        public MapProfile MapProfile { get; private set; }
        public bool IsLoadCompleted { get; private set; }
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();

            _assetLoaderContainer = new AddressablesLoaderContainer();
            _assetLoaderContainer.LoadCompleted += OnAssetsLoadCompleted;

            _worldProfilesDic = new Dictionary<string, MapProfile>();
            foreach (var map in _worldProfiles)
                _worldProfilesDic.Add(map.name, map);

            Reset();
        }
        public void Dispose()
        {
            _assetLoaderContainer.LoadCompleted -= OnAssetsLoadCompleted;
        }
        public void LoadLevel(bool showStandby)
        {
            UnloadLevel();
            Stopwatch stopwatch = Stopwatch.StartNew();

            IsLoadCompleted = false;
            StartLoading.Invoke(new StartLoadingEvent(showStandby));

            _root = new GameObject("Map");
            Camera.main.backgroundColor = Color.black;

            LoadAsyncReferences();
            StartCoroutine(CheckLoadCompleted(showStandby, stopwatch));
        }
        public void UnloadLevel()
        {
            if (_root != null)
                Destroy(_root);

            _assetLoaderContainer.Clear();
            _poolService.ClearPool();

            if (!string.IsNullOrWhiteSpace(_nextMap))
                MapProfile = _worldProfilesDic[_nextMap];
        }
        public void SetMap(MapProfile mapProfile) => _nextMap = mapProfile.name;
        public void Reset()
        {
            _nextMap = null;
            MapProfile = _worldProfiles[0];
        }
        #endregion

        #region Private Methods
        private void LoadAsyncReferences()
        {
            foreach (PooledProfileGroup poolGroup in MapProfile.PoolProfiles)
            {
                _assetLoaderContainer.Register(poolGroup.PooledObjectProfiles);
                _assetLoaderContainer.Register(poolGroup.PooledSoundProfiles);
                _assetLoaderContainer.Register(poolGroup.PooledUIProfiles);
            }
            _assetLoaderContainer.Register(MapProfile.name, MapProfile.MapReferences);

            foreach (PooledProfileGroup poolGroup in MapProfile.PoolProfiles)
            {
                _assetLoaderContainer.LoadAssetAsync<GameObject>(poolGroup.PooledObjectProfiles);
                _assetLoaderContainer.LoadAssetAsync<AudioClip>(poolGroup.PooledSoundProfiles);
                _assetLoaderContainer.LoadAssetAsync<GameObject>(poolGroup.PooledUIProfiles);
            }
            _assetLoaderContainer.LoadAssetAsync<GameObject>(MapProfile.name);
        }
        private void OnAssetsLoadCompleted()
        {
            var mapReference = _assetLoaderContainer.GetAssetReference<GameObject>(MapProfile.name);
            Instantiate(mapReference, _root.transform);
            Camera.main.backgroundColor = MapProfile.BackgroundColor;

            IsLoadCompleted = true;
        }
        private IEnumerator CheckLoadCompleted(bool showStandby, Stopwatch stopwatch)
        {
            if (showStandby)
                yield return new WaitUntil(() => stopwatch.Elapsed.TotalSeconds >= _standbyDelay);
            stopwatch.Stop();

            yield return new WaitUntil(() => IsLoadCompleted);
            LoadCompleted.Invoke();
        }
        #endregion

        #region Structures
        public struct StartLoadingEvent
        {
            public bool ShowStandby;

            public StartLoadingEvent(bool showStandby)
            {
                ShowStandby = showStandby;
            }
        }
        #endregion
    }
}