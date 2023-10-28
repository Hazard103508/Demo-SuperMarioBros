using Mario.Application.Interfaces;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class GameplayService : MonoBehaviour, IGameplayService
    {
        #region Events
        public event Action GameFreezed;
        public event Action GameUnfreezed;
        #endregion

        #region Public Methods
        public void Initalize()
        {
        }
        public void Dispose()
        {
        }
        public void FreezeGame() => GameFreezed?.Invoke();
        public void UnfreezeGame() => GameUnfreezed?.Invoke();
        #endregion
    }
}