using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject _blackScreen; // pantalla de carga falsa para simular version de nes

        private void Awake()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.CurrentMapProfile.MapInit.BackgroundColor;
            AllServices.GameDataService.IsMapCompleted = false;
            AllServices.TimeService.ResetTimer();

            foreach (var mapSection in AllServices.GameDataService.CurrentMapProfile.MapsSections)
                LoadMapSection(mapSection);

            StartCoroutine(StartGame());
        }

        private void OnDestroy()
        {
            SetNextMap();
            AllServices.SceneService.ReleaseAllAssets();
        }
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
        private void LoadMapSection(MapSection mapSection)
        {
            var _mapSection = Instantiate(mapSection.Reference, transform);
            _mapSection.transform.position = Vector3.right * mapSection.InitXPosition;
            var unloader = _mapSection.AddComponent<MapSectionUnloader>();
            unloader.Width = mapSection.Width;
        }

        private IEnumerator StartGame()
        {
            _blackScreen.SetActive(true);
            yield return new WaitForSeconds(AllServices.GameDataService.CurrentMapProfile.MapInit.BlackScreenTime); // fuerza una pantalla negra de demora
            _blackScreen.SetActive(false);

            AllServices.PlayerService.CanMove = true;
            AllServices.TimeService.StartTimer();
        }
    }
}