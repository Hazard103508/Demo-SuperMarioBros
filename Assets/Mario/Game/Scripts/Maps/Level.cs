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

        [SerializeField] private PlayerController _playerController;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();

            _playerService.SetPlayer(_playerController);
            _levelService.LoadLevel();
        }
        private void OnDestroy()
        {
            _levelService.UnloadLevel();
        }
        #endregion
    }
}