using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : TriggerPoint
    {
        #region Objects
        private ILevelService _levelService;

        [SerializeField] private int _checkPointIndex;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }
        #endregion

        #region Protected Methods
        protected override void OnHitCheckPoint(PlayerController player)
        {
            if (_levelService.CurrentMapProfile.CheckPoints.Length > _checkPointIndex)
                _levelService.NextMapProfile = _levelService.CurrentMapProfile.CheckPoints[_checkPointIndex];

            base.OnHitCheckPoint(player);
        }
        #endregion
    }
}