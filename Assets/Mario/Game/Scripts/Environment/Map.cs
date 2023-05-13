using UnityEngine;

namespace Mario.Game.Environment
{
    public class Map : MonoBehaviour
    {
        private void Start()
        {
            Camera.main.backgroundColor = AllServices.GameDataService.MapProfile.BackgroundColor;
        }
    }
}