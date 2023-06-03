using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, ILeftHitable
    {
        [SerializeField] private FlagPoleProfile _profile;
        [SerializeField] private GameObject _flag;
        [SerializeField] private AudioSource _audioSource;

        private bool _isLowering;

        public void OnHitFromLeft(PlayerController player)
        {
            ShowPoint(player);
            LowerFlag(player);
        }
        public void ShowPoint(PlayerController player)
        {
            if (!_isLowering)
            {
                int point = GetFlagPoints(player);
                AllServices.ScoreService.Add(point);

                var position = transform.position + new Vector3(0.75f, 0, 0);
                AllServices.ScoreService.ShowPoint(point, position, 1, 8, false);
            }
        }
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
            AllServices.MusicService.Stop();
            AllServices.PlayerService.CanMove = false;

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

            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.EndPoint.VictoryTheme;
            AllServices.MusicService.Play();
            AllServices.PlayerService.CanMove = true;
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
        private int GetFlagPoints(PlayerController player)
        {
            var hitPoint = 1 - Mathf.InverseLerp(transform.position.y, transform.position.y + 10, player.transform.position.y);
            int index = Mathf.FloorToInt(Mathf.Clamp(hitPoint * _profile.Points.Length, 0, _profile.Points.Length));
            return _profile.Points[index];
        }
    }
}