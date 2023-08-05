using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using System;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapInitializer : MonoBehaviour
    {
        #region Objects
        [SerializeField] private PlayerController _player;
        [SerializeField] private GameObject _blackScreen; // pantalla de carga falsa para simular version de nes
        #endregion

        #region Unity Methods
        private void Awake()
        {
            AllServices.TimeService.ResetTimer();
            AllServices.GameDataService.IsGoalReached = false;
            AllServices.PlayerService.OnLivesRemoved.AddListener(OnLivesRemoved);

            Camera.main.backgroundColor = AllServices.GameDataService.CurrentMapProfile.MapInit.BackgroundColor;
            LoadMapSection();
            LoadObjectsPool();

            StartCoroutine(StartGame());
        }
        private void OnDestroy()
        {
            SetNextMap();
            AllServices.AddressablesService.ReleaseAllAssets();
            AllServices.PlayerService.OnLivesRemoved.RemoveListener(OnLivesRemoved);
        }
        #endregion

        #region Private Methods
        private void SetNextMap()
        {
            if (AllServices.GameDataService.NextMapProfile != null)
            {
                AllServices.TimeService.StartTime =
                    AllServices.GameDataService.NextMapProfile.Time.Type == MapTimeType.Continuated ? AllServices.TimeService.Time :
                    AllServices.GameDataService.NextMapProfile.Time.Type == MapTimeType.Beginning ? AllServices.GameDataService.NextMapProfile.Time.StartTime :
                    0;

                AllServices.GameDataService.CurrentMapProfile = AllServices.GameDataService.NextMapProfile;
                AllServices.GameDataService.NextMapProfile = null;
            }
        }
        private void LoadMapSection()
        {
            int positionX = 0;
            foreach (var mapSection in AllServices.GameDataService.CurrentMapProfile.MapSectionReferences)
                LoadMapSection(mapSection, ref positionX);

            AllServices.GameDataService.CurrentMapProfile.Width = positionX;
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
            LoadObjectsPool(AllServices.GameDataService.CurrentMapProfile.ObjectsPool.ItemsPool);
        }
        private void LoadObjectsPool(ObjectPoolItem[] poolItems) => Array.ForEach(poolItems, item => AllServices.AddressablesService.AddAsset(item.Reference));
        private IEnumerator StartGame()
        {
            _blackScreen.SetActive(true);
            yield return new WaitForSeconds(AllServices.GameDataService.CurrentMapProfile.MapInit.BlackScreenTime); // fuerza una pantalla negra de demora
            _blackScreen.SetActive(false);

            if (AllServices.GameDataService.CurrentMapProfile.MapInit.StartLocation == Enums.PlayerStartLocation.Pipe)
            {
                yield return new WaitForSeconds(1);

                while (_player.transform.position.y < AllServices.GameDataService.CurrentMapProfile.MapInit.StartPosition.y + 2)
                {
                    _player.transform.Translate(Vector3.up * Time.deltaTime * 2f);
                    yield return null;
                }
            }
            else if (AllServices.GameDataService.CurrentMapProfile.MapInit.StartLocation == Enums.PlayerStartLocation.Falling)
            {
                _player.Input.X = 1;
                yield return new WaitForEndOfFrame();
            }

            AllServices.PlayerService.CanMove = true;
            AllServices.TimeService.StartTimer();
        }
        private IEnumerator ReloadMap()
        {
            yield return new WaitForSeconds(3.5f);

            if (AllServices.PlayerService.Lives <= 0)
                AllServices.SceneService.LoadGameOverScene();
            else if (AllServices.TimeService.Time == 0)
                AllServices.SceneService.LoadTimeUpScene();
            else
                AllServices.SceneService.LoadStandByScene();
        }
        #endregion

        #region Service Events	
        private void OnLivesRemoved()
        {
            AllServices.TimeService.StopTimer();
            AllServices.PlayerService.CanMove = false;

            StartCoroutine(ReloadMap());
        }
        #endregion
    }
}