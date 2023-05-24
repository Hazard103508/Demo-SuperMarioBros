using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapInitializer : MonoBehaviour
    {
        private void Awake()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.CurrentMapProfile.BackgroundColor;
            AllServices.CharacterService.ResumeMovement();
            AllServices.GameDataService.IsMapCompleted = false;

            foreach (var mapSection in AllServices.GameDataService.CurrentMapProfile.MapsSections)
                LoadMapSection(mapSection);
        }

        private void OnDestroy()
        {
            if (AllServices.GameDataService.NextMapProfile != null)
            {
                AllServices.GameDataService.CurrentMapProfile = AllServices.GameDataService.NextMapProfile;
                AllServices.GameDataService.NextMapProfile = null;
            }

            AllServices.AssetReferencesService.ReleaseAllAsset();
        }

        private void LoadMapSection(MapSection mapSection)
        {
            var _mapSection = Instantiate(mapSection.Reference, transform);
            _mapSection.transform.position = Vector3.right * mapSection.InitXPosition;
            var unloader = _mapSection.AddComponent<MapSectionUnloader>();
            unloader.Width = mapSection.Width;
        }
    }
}