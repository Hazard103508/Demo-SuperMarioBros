using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class PipeDown : MonoBehaviour, ITopHitable
    {
        [SerializeField] private PipeProfile _pipeProfile;
        [SerializeField] private AudioSource _pipeInSoundFX;
        private bool _isInPipe;

        public void OnHitFromTop(PlayerController player)
        {
            if (_isInPipe)
                return;

            if (player.Input.IsDucking)
                StartCoroutine(MoveIntPipe(player));
        }

        private IEnumerator MoveIntPipe(PlayerController player)
        {
            AllServices.TimeService.StopTimer();
            AllServices.PlayerService.CanMove = false;
            AllServices.GameDataService.NextMapProfile = _pipeProfile.MapProfile;
            AllServices.SceneService.LoadMapScene(0.8f);

            _isInPipe = true;
            _pipeInSoundFX.Play();

            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(Vector3.down * Time.deltaTime * 4f);
                yield return null;
            }
        }
    }
}