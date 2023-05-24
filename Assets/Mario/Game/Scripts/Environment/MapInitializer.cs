using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapInitializer : MonoBehaviour
    {
        private void Awake()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.CurrentMapProfile.BackgroundColor;
            AllServices.TimeService.ResetTimer();
            AllServices.TimeService.StartTimer();
            AllServices.CharacterService.ResumeMovement();

            foreach (var mapSection in AllServices.GameDataService.CurrentMapProfile.MapsSections)
                LoadMapSection(mapSection);
        }

        private void Update() => AllServices.TimeService.UpdateTimer();
        private void OnDestroy()
        {
            Array.ForEach(AllServices.GameDataService.CurrentMapProfile.MapsSections, m => m.Reference.ReleaseAsset());
            AllServices.AssetReferencesService.ReleaseAllAsset();
        }

        private void LoadMapSection(MapSection mapSection)
        {
            var asyncOperationHandle = mapSection.Reference.LoadAssetAsync<GameObject>();
            asyncOperationHandle.Completed += handle =>
            {
                var _mapSection = Instantiate(handle.Result, transform);
                _mapSection.transform.position = Vector3.right * mapSection.InitXPosition;
                var unloader = _mapSection.AddComponent<MapSectionUnloader>();
                unloader.Width = mapSection.Width;
            };
        }
    }
}