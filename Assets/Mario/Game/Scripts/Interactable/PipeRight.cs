using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeRight : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
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
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region Protected Methods
        private IEnumerator MoveIntoPipe(PlayerController player)
        {
            _timeService.FreezeTimer();

            _isInPipe = true;
            player.Movable.enabled = false;
            //player.IsAutoWalk = true;

            _soundService.Play(_profile.SoundFXPoolReference);
            while (player.transform.position.x < transform.position.x)
            {
                player.transform.Translate(2f * Time.deltaTime * Vector3.right);
                yield return null;
            }

            player.transform.position = new Vector3(Mathf.Round(player.transform.position.x), player.transform.position.y, player.transform.position.z);
            _levelService.SetNextMap(_profile.Connection);
            _sceneService.LoadMapScene(0.2f);
            //player.IsAutoWalk = false;
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            if (_isInPipe)
                return;

            StartCoroutine(MoveIntoPipe(player));
        }
        #endregion
    }
}