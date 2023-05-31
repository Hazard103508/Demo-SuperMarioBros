using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapCheckPoint : MonoBehaviour
    {
        private void Awake()
        {
            AllServices.PlayerService.OnPlayerPositionChanged.AddListener(OnPlayerPositionChanged);

            if (AllServices.GameDataService.CurrentMapProfile.CheckPoint.mapProfile == null)
                Destroy(this);
        }
        private void OnDestroy()
        {
            AllServices.PlayerService.OnPlayerPositionChanged.RemoveListener(OnPlayerPositionChanged);
        }
        public void OnPlayerPositionChanged(Vector3 position)
        {
            if (position.x >= AllServices.GameDataService.CurrentMapProfile.CheckPoint.PositionX)
            {
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.CheckPoint.mapProfile;
                Destroy(this);
            }
        }
    }
}