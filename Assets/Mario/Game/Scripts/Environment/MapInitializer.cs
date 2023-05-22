using Mario.Application.Services;
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
        }
        private void Update() => AllServices.TimeService.UpdateTimer();
        private void OnDestroy() => AllServices.AssetReferencesService.ReleaseAllAsset();
    }
}