using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class Map : MonoBehaviour
    {
        private void Awake()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.MapProfile.BackgroundColor;
            AllServices.TimeService.ResetTimer();
            AllServices.TimeService.StartTimer();
        }
        private void Update()
        {
            AllServices.TimeService.UpdateTimer();
        }

    }
}