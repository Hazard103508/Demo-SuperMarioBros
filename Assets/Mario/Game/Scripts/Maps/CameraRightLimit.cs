using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class CameraRightLimit : MonoBehaviour
    {
        #region Objects
        private int limitXPosition;
        #endregion

        #region Unity Methods
        private void Start()
        {
            limitXPosition = Services.GameDataService.CurrentMapProfile.Width;
        }
        private void LateUpdate()
        {

            var cam = Camera.main;
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (topRight.x > limitXPosition)
                this.transform.position -= Vector3.right * (topRight.x - limitXPosition);
        }
        #endregion
    }
}