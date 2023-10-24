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
        private ISceneService _sceneService;
        private ITimeService _timeService;
        private ILevelService _levelService;
        private ISoundService _soundService;

        [SerializeField] private PipeProfile _profile;
        private bool _isInPipe;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
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
            _timeService.FreezeTimer();

            _isInPipe = true;
            _playerService.EnablePlayerMovable(false);
            _playerService.EnablePlayerController(false);

            _soundService.Play(_profile.SoundFXPoolReference);
            yield return OnMovePlayer(player);

            player.transform.position = new Vector3(player.transform.position.x, Mathf.Round(player.transform.position.y), player.transform.position.z);
            _levelService.SetNextMap(_profile.Connection);
            _levelService.LoadNextLevel();
        }
        #endregion
    }
}