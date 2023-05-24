using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, ILeftHitable
    {
        [SerializeField] private GameObject _flag;
        [SerializeField] private AudioSource _audioSource;

        private bool _isLowering;

        public void OnHitFromLeft(PlayerController player) => LowerFlag(player);
        public void LowerFlag(PlayerController player)
        {
            if (!_isLowering)
            {
                _isLowering = true;
                AllServices.TimeService.StopTimer();


                player.transform.localScale = Vector3.one;
                player.transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);

                var _playerAnimator = player.GetComponentInChildren<PlayerAnimator>();

                _audioSource.Play();
                StartCoroutine(DownPlayerPole(player, _playerAnimator));
                StartCoroutine(DownFlagPole(player, _playerAnimator));
                AllServices.GameDataService.IsMapCompleted = true;
            }
        }
        private IEnumerator DownFlagPole(PlayerController player, PlayerAnimator animator)
        {
            AllServices.CharacterService.StopMovement();

            while (_flag.transform.position.y > transform.position.y + 0.5f)
            {
                _flag.transform.Translate(Vector3.down * Time.deltaTime * 9);
                yield return null;
            }

            player.transform.Find("Visual").localScale = new Vector3(-1, 1, 1);
            player.transform.Translate(Vector3.right);

            yield return new WaitForSeconds(0.4f);
            animator.IsInFlagPole = false;
            player.IsAutoWalk = true;

            AllServices.CharacterService.ResumeMovement();
        }
        private IEnumerator DownPlayerPole(PlayerController player, PlayerAnimator animator)
        {
            animator.IsInFlagPole = true;
            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(Vector3.down * Time.deltaTime * 9);
                yield return null;
            }

            animator.IsInFlagBase = true;
        }
    }
}