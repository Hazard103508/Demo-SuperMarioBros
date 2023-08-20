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
        [SerializeField] private int _pipeIndex;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;
        #endregion

        #region Private Methods
        private IEnumerator MoveIntoPipe(PlayerController_OLD player)
        {
            Services.TimeService.StopTimer();
            Services.PlayerService.CanMove = false;
            Services.GameDataService.NextMapProfile = Services.GameDataService.CurrentMapProfile.PipesConnections[_pipeIndex];
            Services.SceneService.LoadMapScene(0.8f);

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
        public void OnHittedByPlayerFromTop(PlayerController_OLD player)
        {
            if (_isInPipe)
                return;

            if (player.Input.IsDucking)
                StartCoroutine(MoveIntoPipe(player));
        }
        #endregion
    }
}