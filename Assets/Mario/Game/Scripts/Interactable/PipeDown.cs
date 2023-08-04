using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeDown : MonoBehaviour, IHitableByPlayerFromTop
    {
        #region Objects
        [SerializeField] private int _pipeIndex;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;
        #endregion

        #region Private Methods
        private IEnumerator MoveIntPipe(PlayerController player)
        {
            AllServices.TimeService.StopTimer();
            AllServices.PlayerService.CanMove = false;
            AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.PipesConnections[_pipeIndex];
            AllServices.SceneService.LoadMapScene(0.8f);

            _isInPipe = true;
            _pipeInSoundFX.Play();

            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(Vector3.down * Time.deltaTime * 4f);
                yield return null;
            }

            player.transform.position = new Vector3(player.transform.position.x, Mathf.Round(player.transform.position.y), player.transform.position.z);
        }
        #endregion

        #region On Player Hit
        public void OnHitableByPlayerFromTop(PlayerController player)
        {
            if (_isInPipe)
                return;

            if (player.Input.IsDucking)
                StartCoroutine(MoveIntPipe(player));
        }
        #endregion
    }
}