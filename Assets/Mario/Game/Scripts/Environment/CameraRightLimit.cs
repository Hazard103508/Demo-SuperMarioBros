using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class CameraRightLimit : MonoBehaviour
    {
        private int limitXPosition;

        private void Start()
        {
            limitXPosition = AllServices.GameDataService.CurrentMapProfile.Width;
        }
        private void LateUpdate()
        {

            var cam = Camera.main;
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (topRight.x > limitXPosition)
                this.transform.position -= Vector3.right * (topRight.x - limitXPosition);
        }
    }
}