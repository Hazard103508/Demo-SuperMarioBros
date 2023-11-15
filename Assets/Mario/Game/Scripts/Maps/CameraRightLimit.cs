using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class CameraRightLimit : MonoBehaviour
    {
        #region Objects
        private ILevelService _levelService;
        #endregion

        #region Unity Methods
        private void Start()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }
        private void LateUpdate()
        {
            if (_levelService.MapProfile.Width == 0)
            {
                return;
            }

            var cam = Camera.main;
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (topRight.x > _levelService.MapProfile.Width)
                this.transform.position -= Vector3.right * (topRight.x - _levelService.MapProfile.Width);
        }
        #endregion
    }
}