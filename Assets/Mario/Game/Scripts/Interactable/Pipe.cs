using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class Pipe : MonoBehaviour
    {
        #region Objects
        protected IPlayerService _playerService;
        private ILevelService _levelService;
        private ISoundService _soundService;
        private IGameplayService _gameplayService;

        [SerializeField] private PipeProfile _profile;
        private bool _isInPipe;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Protected Methods
        protected void MoveIntoPipe(PlayerController player)
        {
            if (_isInPipe)
                return;

            StartCoroutine(MoveIntoPipeCO(player));
        }
        protected virtual IEnumerator OnMovePlayer(PlayerController player)
        {
            yield return null;
        }
        #endregion

        #region Private Methods
        private IEnumerator MoveIntoPipeCO(PlayerController player)
        {
            _gameplayService.FreezeGame();

            _isInPipe = true;
            _soundService.Play(_profile.SoundFXPoolReference);
            yield return OnMovePlayer(player);

            player.transform.position = Vector3.zero;
            _gameplayService.SetNextMap(_profile.Connection);
            _levelService.LoadLevel(false);
        }
        #endregion
    }
}