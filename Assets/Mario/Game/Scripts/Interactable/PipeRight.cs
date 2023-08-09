using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeRight : MonoBehaviour, IHitableByPlayerFromLeft
    {
        #region Objects
        [SerializeField] private int _pipeIndex;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;
        #endregion

        #region Protected Methods
        private IEnumerator MoveIntoPipe(PlayerController player)
        {
            Services.TimeService.StopTimer();
            Services.PlayerService.CanMove = false;
            Services.GameDataService.NextMapProfile = Services.GameDataService.CurrentMapProfile.PipesConnections[_pipeIndex];
            Services.SceneService.LoadMapScene(2.8f);

            _isInPipe = true;
            _pipeInSoundFX.Play();

            player.IsAutoWalk = true;
            Destroy(player.GetComponent<PlayerCollisions>());
            while (player.transform.position.x < transform.position.x)
            {
                player.transform.Translate(2f * Time.deltaTime * Vector3.right);
                yield return null;
            }

            player.transform.position = new Vector3(Mathf.Round(player.transform.position.x), player.transform.position.y, player.transform.position.z);
            player.IsAutoWalk = false;
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            if (_isInPipe)
                return;

            if (player.IsGrounded && !player.Input.Jump)
                StartCoroutine(MoveIntoPipe(player));
        }
        #endregion
    }
}