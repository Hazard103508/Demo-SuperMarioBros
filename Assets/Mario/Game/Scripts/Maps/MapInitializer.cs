using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using System;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class MapInitializer : MonoBehaviour
    {
        #region Objects
        [SerializeField] private PlayerController_OLD _player;
        [SerializeField] private GameObject _blackScreen; // pantalla de carga falsa para simular version de nes
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.TimeService.ResetTimer();
            Services.GameDataService.IsGoalReached = false;
            Services.PlayerService.LivesRemoved += OnLivesRemoved;

            Camera.main.backgroundColor = Services.GameDataService.CurrentMapProfile.MapInit.BackgroundColor;
            LoadMapSection();
            LoadObjectsPool();

            StartCoroutine(StartGame());
        }
        private void OnDestroy()
        {
            SetNextMap();
            Services.AddressablesService.ReleaseAllAssets();
            Services.PoolService.ClearPool();
            Services.PlayerService.LivesRemoved -= OnLivesRemoved;
        }
        #endregion

        #region Private Methods
        private void SetNextMap()
        {
            if (Services.GameDataService.NextMapProfile != null)
            {
                Services.TimeService.StartTime =
                    Services.GameDataService.NextMapProfile.Time.Type == MapTimeType.Continuated ? Services.TimeService.Time :
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
        private void LoadObjectsPool()
        {
            LoadObjectsPool<GameObject>(Services.GameDataService.CurrentMapProfile.PoolProfile.ObjectPool);
            LoadObjectsPool<AudioClip>(Services.GameDataService.CurrentMapProfile.PoolProfile.SoundPool);
            LoadObjectsPool<GameObject>(Services.GameDataService.CurrentMapProfile.PoolProfile.UIPool);
        }
        private void LoadObjectsPool<T>(BasePooledObjectProfile[] poolItems) => Array.ForEach(poolItems, item => Services.AddressablesService.AddAsset<T>(item.Reference));
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
                _player.Input.X = 1;
                yield return new WaitForEndOfFrame();
            }

            Services.PlayerService.CanMove = true;
            Services.TimeService.StartTimer();
        }
        private IEnumerator ReloadMap()
        {
            yield return new WaitForSeconds(3.5f);

            if (Services.PlayerService.Lives <= 0)
                Services.SceneService.LoadGameOverScene();
            else if (Services.TimeService.Time == 0)
                Services.SceneService.LoadTimeUpScene();
            else
                Services.SceneService.LoadStandByScene();
        }
        #endregion

        #region Service Events	
        private void OnLivesRemoved()
        {
            Services.TimeService.StopTimer();
            Services.PlayerService.CanMove = false;

            StartCoroutine(ReloadMap());
        }
        #endregion
    }
}