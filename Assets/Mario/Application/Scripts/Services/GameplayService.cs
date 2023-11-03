using Mario.Application.Interfaces;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class GameplayService : MonoBehaviour, IGameplayService
    {
        #region Object
        private ITimeService _timeService;
        #endregion

        #region Events
        public event Action GameFreezed;
        public event Action GameUnfreezed;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _timeService = ServiceLocator.Current.Get<ITimeService>();
        }
        public void Dispose()
        {
        }
        public void FreezeGame()
        {
            _timeService.FreezeTimer();
            GameFreezed?.Invoke();
        }
        public void UnfreezeGame()
        {
            _timeService.UnfreezeTimer();
            GameUnfreezed?.Invoke();
        }
        #endregion
    }
}