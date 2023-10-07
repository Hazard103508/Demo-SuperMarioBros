using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.Maps
{
    public class MapInitializer : MonoBehaviour
    {
        #region Objects
        private IAddressablesService _addressablesService;
        private IPoolService _poolService;
        private ISceneService _sceneService;
        private IPlayerService _playerService;
        private ITimeService _timeService;

        [SerializeField] private PlayerController _player;
        [SerializeField] private GameObject _blackScreen; // pantalla de carga falsa para simular version de nes
        #endregion


        #region Unity Methods
        private void Awake()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
            _poolService = ServiceLocator.Current.Get<IPoolService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();

            _timeService.ResetTimer();
            Services.GameDataService.IsGoalReached = false;
            _playerService.LivesRemoved += OnLivesRemoved;

            Camera.main.backgroundColor = Services.GameDataService.CurrentMapProfile.MapInit.BackgroundColor;
            LoadMapSection();
            LoadAssets();

            StartCoroutine(StartGame());
        }
        private void OnDestroy()
        {
            SetNextMap();
            _addressablesService.ReleaseAllAssets();
            _poolService.ClearPool();
            _playerService.LivesRemoved -= OnLivesRemoved;
        }
        #endregion

        #region Private Methods
        private void SetNextMap()
        {
            if (Services.GameDataService.NextMapProfile != null)
            {
                _timeService.StartTime =
                    Services.GameDataService.NextMapProfile.Time.Type == MapTimeType.Continuated ? _timeService.Time :
                    Services.GameDataService.NextMapProfile.Time.Type == MapTimeType.Beginning ? Services.GameDataService.NextMapProfile.Time.StartTime :
                    0;

                Services.GameDataService.CurrentMapProfile = Services.GameDataService.NextMapProfile;
                Services.GameDataService.NextMapProfile = null;
            }
        }
        private void LoadMapSection()
        {
            int positionX = 0;
            foreach (var mapSection in Services.GameDataService.CurrentMapProfile.MapSectionReferences)
                LoadMapSection(mapSection, ref positionX);

            Services.GameDataService.CurrentMapProfile.Width = positionX;
        }
        private void LoadMapSection(GameObject mapSectionReference, ref int positionX)
        {
            var mapObj = Instantiate(mapSectionReference, transform);
            mapObj.transform.position = Vector3.right * positionX;

            var mapSection = mapObj.GetComponent<MapSection>();

            var unloader = mapObj.AddComponent<MapSectionUnloader>();
            unloader.Width = mapSection.Size.Width;

            positionX += mapSection.Size.Width;
        }
        private void LoadAssets()
        {
            foreach (PooledProfileGroup poolGroup in Services.GameDataService.CurrentMapProfile.PoolProfiles)
            {
                LoadObjectsPool<GameObject, PooledObjectProfile>(poolGroup.PooledObjectProfiles, profile => profile.Reference);
                LoadObjectsPool<AudioClip, PooledSoundProfile>(poolGroup.PooledSoundProfiles, profile => profile.Reference);
                LoadObjectsPool<GameObject, PooledUIProfile>(poolGroup.PooledUIProfiles, profile => profile.Reference);
            }
        }
        private void LoadObjectsPool<T, R>(R[] poolItems, Func<R, AssetReference> getReferenceFunc) where R : PooledBaseProfile
        {
            Array.ForEach(poolItems, item => _addressablesService.LoadAssetAsync<T>(getReferenceFunc(item)));
        }

        private IEnumerator StartGame()
        {
            _blackScreen.SetActive(true);
            yield return new WaitForSeconds(Services.GameDataService.CurrentMapProfile.MapInit.BlackScreenTime); // fuerza una pantalla negra de demora
            _blackScreen.SetActive(false);

            if (Services.GameDataService.CurrentMapProfile.MapInit.StartLocation == Enums.PlayerStartLocation.Pipe)
            {
                yield return new WaitForSeconds(1);

                while (_player.transform.position.y < Services.GameDataService.CurrentMapProfile.MapInit.StartPosition.y + 2)
                {
                    _player.transform.Translate(Vector3.up * Time.deltaTime * 2f);
                    yield return null;
                }
            }
            else if (Services.GameDataService.CurrentMapProfile.MapInit.StartLocation == Enums.PlayerStartLocation.Falling)
            {
                //_player.InputActions.Move.x = 1; // TODO
                yield return new WaitForEndOfFrame();
            }

            _timeService.StartTimer();
        }
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
        #endregion

        #region Service Events	
        private void OnLivesRemoved()
        {
            _timeService.StopTimer();
            StartCoroutine(ReloadMap());
        }
        #endregion
    }
}