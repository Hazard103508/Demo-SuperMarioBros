using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeRight : MonoBehaviour, ILeftHitable
    {
        [SerializeField] private int _pipeIndex;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;

        public void OnHitFromLeft(PlayerController player)
        {
            if (_isInPipe)
                return;

            if (player.IsGrounded && !player.Input.JumpDown)
                StartCoroutine(MoveIntPipe(player));
        }

        private IEnumerator MoveIntPipe(PlayerController player)
        {
            AllServices.TimeService.StopTimer();
            AllServices.PlayerService.CanMove = false;
            AllServices.GameDataService.NextMapProfile = AllServices.GameDataService.CurrentMapProfile.PipesConnections[_pipeIndex];
            AllServices.SceneService.LoadMapScene(2.8f);

            _isInPipe = true;
            _pipeInSoundFX.Play();

            player.IsAutoWalk = true;
            Destroy(player.GetComponent<PlayerCollisions>());
            while (player.transform.position.x < transform.position.x)
            {
                player.transform.Translate(Vector3.right * Time.deltaTime * 2f);
                yield return null;
            }

            player.transform.position = new Vector3(Mathf.Round(player.transform.position.x), player.transform.position.y, player.transform.position.z);
            player.IsAutoWalk = false;
        }
    }
}