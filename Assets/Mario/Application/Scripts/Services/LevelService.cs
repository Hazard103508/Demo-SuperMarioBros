using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        private IPoolService _poolService;

        [SerializeField] private MapProfile _currentMapProfile;
        private AddressablesLoaderContainer _assetLoaderContainer;
        private GameObject _root;
        private NextMap _nextMap;
        #endregion

        #region Events
        public event Action StartLoading;
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

            MapProfile = _currentMapProfile;
            _assetLoaderContainer = new AddressablesLoaderContainer();
            _assetLoaderContainer.LoadCompleted += OnAssetsLoadCompleted;
        }
        public void Dispose()
        {
            _assetLoaderContainer.LoadCompleted -= OnAssetsLoadCompleted;
        }
        public void LoadLevel()
        {
            IsLoadCompleted = false;
            StartLoading.Invoke();

            _root = new GameObject("Map");
            Camera.main.backgroundColor = Color.black;

            StartCoroutine(LoadAsyncReferences());
        }
        public void LoadNextLevel()
        {
            UnloadLevel();
            LoadLevel();
        }
        public void UnloadLevel()
        {
            if (_root != null)
                Destroy(_root);

            _assetLoaderContainer.Clear();
            _poolService.ClearPool();
        }
        public void SetMap(string mapName)
        {
            _nextMap = new NextMap();
            var asyncOperationHandle = Addressables.LoadAssetAsync<MapProfile>(mapName);
            asyncOperationHandle.Completed += handle => 
            {
                _nextMap.Profile = handle.Result;
                //Addressables.Release(handle);
            };
        }
        #endregion

        #region Private Methods
        private IEnumerator LoadAsyncReferences()
        {
            if (_nextMap != null && _nextMap.Profile == null)
                yield return new WaitUntil(() => _nextMap.Profile != null);

            if (_nextMap != null && _nextMap.Profile != null)
            {
                MapProfile = _nextMap.Profile;
                _nextMap = null;
            }

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
            LoadCompleted.Invoke();
        }
        #endregion

        #region Structures
        public class NextMap
        {
            public string Name { get; set; }
            public MapProfile Profile { get; set; }
        }
        #endregion
    }
}