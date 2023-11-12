using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        private IPoolService _poolService;

        [SerializeField] private MapProfile _currentMapProfile;
        private AddressablesLoaderContainer _assetLoaderContainer;
        private GameObject _root;
        #endregion

        #region Events
        public event Action StartLoading;
        public event Action LoadCompleted;
        #endregion

        #region Properties
        public MapProfile MapProfile { get; private set; }
        public bool IsLoadCompleted { get; private set; }
        public Func<MapProfile> GetMapConnection { get; set; }
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

            var mapConnectionProfile = GetMapConnection.Invoke();
            if (mapConnectionProfile != null)
                MapProfile = mapConnectionProfile;

            _root = new GameObject("Map");
            Camera.main.backgroundColor = Color.black;

            LoadAsyncReferences();
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
            LoadCompleted.Invoke();
        }
        #endregion
    }
}