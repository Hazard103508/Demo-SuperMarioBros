using UnityEngine;

namespace Mario.Game.Environment
{
    public class Map : MonoBehaviour
    {
        private void Awake()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.MapProfile.BackgroundColor;
            AllServices.TimeService.Reset();
            AllServices.TimeService.Enabled= true;
        }
        private void Update()
        {
            AllServices.TimeService.UpdateTime();
        }
    
    }
}