using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeDown : MonoBehaviour, IHittableByPlayerFromTop
    {
        #region Objects
        private ISceneService _sceneService;
        private ITimeService _timeService;
        private ILevelService _levelService;

        [SerializeField] private int _pipeIndex;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();
        }
        #endregion

        #region Private Methods
        private IEnumerator MoveIntoPipe(PlayerController player)
        {
            _timeService.FreezeTimer();
            _levelService.NextMapProfile = _levelService.CurrentMapProfile.PipesConnections[_pipeIndex];
            _sceneService.LoadMapScene(0.8f);

            _isInPipe = true;
            _pipeInSoundFX.Play();

            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(4f * Time.deltaTime * Vector3.down);
                yield return null;
            }

            player.transform.position = new Vector3(player.transform.position.x, Mathf.Round(player.transform.position.y), player.transform.position.z);
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player)
        {
            if (_isInPipe)
                return;

            if (player.InputActions.Ducking)
                StartCoroutine(MoveIntoPipe(player));
        }
        #endregion
    }
}