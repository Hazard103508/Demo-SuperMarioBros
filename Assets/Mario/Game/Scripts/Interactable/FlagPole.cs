using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
        [SerializeField] private FlagPoleProfile _profile;
        [SerializeField] private GameObject _flag;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private FlagScoreLabel _scoreLabel;
        private bool _isLowering;
        #endregion

        #region Private Methods
        private void ShowPoint(PlayerController_OLD player)
        {
            if (!_isLowering)
            {
                int point = GetFlagPoints(player);
                Services.ScoreService.Add(point);
                _scoreLabel.ShowPoint(point);
            }
        }
        private void LowerFlag(PlayerController_OLD player)
        {
            if (!_isLowering)
            {
                _isLowering = true;
                Services.TimeService.StopTimer();


                player.transform.localScale = Vector3.one;
                player.transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);

                var _playerAnimator = player.GetComponentInChildren<PlayerAnimator>();

                _audioSource.Play();
                StartCoroutine(DownPlayerPole(player, _playerAnimator));
                StartCoroutine(DownFlagPole(player, _playerAnimator));

                Services.GameDataService.IsGoalReached = true;
            }
        }
        private IEnumerator DownFlagPole(PlayerController_OLD player, PlayerAnimator animator)
        {
            Services.MusicService.Stop();
            Services.PlayerService.CanMove = false;

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

            Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.VictoryTheme.Clip;
            Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.VictoryTheme.StartTime;
            Services.MusicService.Play();
            Services.PlayerService.CanMove = true;
        }
        private IEnumerator DownPlayerPole(PlayerController_OLD player, PlayerAnimator animator)
        {
            animator.IsInFlagPole = true;
            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(Vector3.down * Time.deltaTime * 9);
                yield return null;
            }

            animator.IsInFlagBase = true;
        }
        private int GetFlagPoints(PlayerController_OLD player)
        {
            var hitPoint = 1 - Mathf.InverseLerp(transform.position.y, transform.position.y + 10, player.transform.position.y);
            int index = Mathf.FloorToInt(Mathf.Clamp(hitPoint * _profile.Points.Length, 0, _profile.Points.Length));
            index = Mathf.Clamp(index, 0, _profile.Points.Length - 1);
            return _profile.Points[index];
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController_OLD player)
        {
            ShowPoint(player);
            LowerFlag(player);
        }
        #endregion
    }
}