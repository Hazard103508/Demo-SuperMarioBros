using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapCheckPoints : MonoBehaviour
    {
        [SerializeField] private Transform PlayerTransform;

        private void Update()
        {
            if (AllServices.GameDataService.CurrentMapProfile.checkPoint.mapProfile == null)
                Destroy(this);

            if (PlayerTransform.position.x >= AllServices.GameDataService.CurrentMapProfile.checkPoint.PositionX)
            {
                AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.checkPoint.mapProfile;
                Destroy(this);
            }
        }
    }
}