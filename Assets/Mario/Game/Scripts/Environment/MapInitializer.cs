using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

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
        private void Update()
        {
            AllServices.TimeService.UpdateTimer();
        }
    }
}