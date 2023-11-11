using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class CheckPoint : MonoBehaviour
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

        #region Public Methods
        public void OnTriggerOn()
        {
            _gameplayService.SetCheckPoint(_checkPointProfile);
        }
        #endregion
    }
}