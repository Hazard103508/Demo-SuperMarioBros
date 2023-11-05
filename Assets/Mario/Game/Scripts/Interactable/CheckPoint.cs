using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : TriggerPoint
    {
        #region Objects
        private IGameplayService _gameplayService;

        [SerializeField] private MapProfile _checkPointProfile;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Protected Methods
        protected override void OnHitCheckPoint(PlayerController player)
        {
            _gameplayService.SetCheckPoint(_checkPointProfile);
            base.OnHitCheckPoint(player);
        }
        #endregion
    }
}