using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapLoader : MonoBehaviour
    {
        [SerializeField] private Transform playerTransform;

        private void Awake()
        {
            foreach (var mapSection in AllServices.GameDataService.MapProfile.MapsSections)
                LoadMapSection(mapSection);
        }
        private void OnDestroy()
        {
            Array.ForEach(AllServices.GameDataService.MapProfile.MapsSections, m => m.Reference.ReleaseAsset());
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