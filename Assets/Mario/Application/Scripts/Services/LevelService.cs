using Mario.Application.Interfaces;
using Mario.Game.Enums;
using Mario.Game.Maps;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Mario.Application.Services
{
    public class LevelService : MonoBehaviour, ILevelService
    {
        #region Objects
        private IAddressablesService _addressablesService;
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
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _poolService = ServiceLocator.Current.Get<IPoolService>();

            MapProfile = _currentMapProfile;
            _assetLoaderContainer = new AddressablesLoaderContainer();
        }
        public void Dispose()
        {
        }
        public async void LoadLevel()
        {
            IsLoadCompleted = false;
            StartLoading.Invoke();

            var mapConnectionProfile = GetMapConnection.Invoke();
            if (mapConnectionProfile != null)
                MapProfile = mapConnectionProfile;

            _root = new GameObject("Map");
            Camera.main.backgroundColor = Color.black;

            LoadAsyncReferences();
            await LoadWorld(_root.transform);

            IsLoadCompleted = true;
            LoadCompleted.Invoke();
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
                _assetLoaderContainer.Register<PooledObjectProfile, GameObject>(poolGroup.PooledObjectProfiles);
                _assetLoaderContainer.Register<PooledSoundProfile, AudioClip>(poolGroup.PooledSoundProfiles);
                _assetLoaderContainer.Register<PooledUIProfile, GameObject>(poolGroup.PooledUIProfiles);
            }

            _assetLoaderContainer.LoadAssetAsync<GameObject>();
            _assetLoaderContainer.LoadAssetAsync<AudioClip>();
        }
        private async Task LoadWorld(Transform parent)
        {
            await _addressablesService.LoadAssetAsync<GameObject>(MapProfile.MapReferences);
            var mapReference = _addressablesService.GetAssetReference<GameObject>(MapProfile.MapReferences);
            Instantiate(mapReference, parent);
            Camera.main.backgroundColor = MapProfile.BackgroundColor;
        }
        //private void LoadMapSection(GameObject mapSectionReference, ref int positionX, Transform parent)
        //{
        //    var mapObj = Instantiate(mapSectionReference, parent);
        //    mapObj.transform.position = Vector3.right * positionX;
        //
        //    var mapSection = mapObj.GetComponent<MapSection>();
        //
        //    var unloader = mapObj.AddComponent<MapSectionUnloader>();
        //    unloader.Width = mapSection.Size.Width;
        //
        //    positionX += mapSection.Size.Width;
        //}
        #endregion
    }
}