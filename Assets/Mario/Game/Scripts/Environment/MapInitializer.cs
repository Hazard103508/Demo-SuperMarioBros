using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerController _player;
        [SerializeField] private GameObject _blackScreen; // pantalla de carga falsa para simular version de nes

        private void Awake()
        {
            AllServices.TimeService.ResetTimer();
            AllServices.GameDataService.IsGoalReached = false;

            Camera.main.backgroundColor = AllServices.GameDataService.CurrentMapProfile.MapInit.BackgroundColor;
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
    }
}