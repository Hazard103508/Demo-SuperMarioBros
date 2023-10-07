using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class Level : MonoBehaviour
    {
        #region Objects
        private ILevelService _levelService;
        private IPlayerService _playerService;

        [SerializeField] private GameObject _blackScreen;
        [SerializeField] private PlayerController _playerController; 
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        private void Start()
        {
            _playerService.PlayerController = _playerController;

            _levelService.BackScreenEnabled += OnBackScreenEnabled;
            _levelService.BackScreenDisabled+= OnBackScreenDisabled;
            _levelService.LoadLevel(transform);
        }
        private void OnDestroy()
        {
            _levelService.BackScreenEnabled -= OnBackScreenEnabled;
            _levelService.BackScreenDisabled -= OnBackScreenDisabled;
            _levelService.UnloadLevel();
        }
        #endregion

        #region Service Meethods
        private void OnBackScreenEnabled() => _blackScreen.SetActive(true);
        private void OnBackScreenDisabled() => _blackScreen.SetActive(false);
        #endregion
    }
}