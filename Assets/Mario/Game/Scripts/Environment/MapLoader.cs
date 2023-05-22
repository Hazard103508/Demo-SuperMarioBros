using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapLoader : MonoBehaviour
    {
        private void Awake()
        {
            if (AllServices.GameDataService.NextMapProfile != null)
            {
                AllServices.GameDataService.CurrentMapProfile = AllServices.GameDataService.NextMapProfile;
                AllServices.GameDataService.NextMapProfile = null;
            }

            foreach (var mapSection in AllServices.GameDataService.CurrentMapProfile.MapsSections)
                LoadMapSection(mapSection);
        }
        private void OnDestroy()
        {
            Array.ForEach(AllServices.GameDataService.CurrentMapProfile.MapsSections, m => m.Reference.ReleaseAsset());
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