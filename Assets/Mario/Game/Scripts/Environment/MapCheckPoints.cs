using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapCheckPoints : MonoBehaviour
    {
        private void Awake()
        {
            AllServices.CharacterService.OnPlayerPositionChanged.AddListener(OnPlayerPositionChanged);
        }
        private void OnDestroy()
        {
            AllServices.CharacterService.OnPlayerPositionChanged.RemoveListener(OnPlayerPositionChanged);
        }
        public void OnPlayerPositionChanged(Vector3 position)
        {
            if (position.x >= AllServices.GameDataService.CurrentMapProfile.checkPoint.PositionX)
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.checkPoint.mapProfile;
        }
    }
}