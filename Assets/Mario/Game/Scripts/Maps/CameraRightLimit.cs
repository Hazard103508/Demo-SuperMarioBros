using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class CameraRightLimit : MonoBehaviour
    {
        #region Objects
        private ILevelService _levelService;

        private int limitXPosition;
        #endregion

        #region Unity Methods
        private void Start()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _levelService.LevelLoaded += OnLevelLoaded;
        }
        private void OnDestroy()
        {
            _levelService.LevelLoaded -= OnLevelLoaded;
        }
        private void LateUpdate()
        {
            if (limitXPosition == 0)
                return;

            var cam = Camera.main;
            var topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, cam.nearClipPlane));

            if (topRight.x > limitXPosition)
                this.transform.position -= Vector3.right * (topRight.x - limitXPosition);
        }
        #endregion

        #region Service Methods
        private void OnLevelLoaded()
        {
            limitXPosition = _levelService.CurrentMapProfile.Width;
        }
        #endregion
    }
}