using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
        private IScoreService _scoreService;

        [SerializeField] private FlagPoleProfile _profile;
        [SerializeField] private GameObject _flag;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private FlagScoreLabel _scoreLabel;
        //private bool _isLowering;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
        }
        #endregion

        #region Private Methods
        private void ShowPoint(PlayerController player)
        {
            //if (!_isLowering)
            //{
            //    int point = GetFlagPoints(player);
            //    _scoreService.Add(point);
            //    _scoreLabel.ShowPoint(point);
            //}
        }
        //private void LowerFlag(PlayerController player)
        //{
        //    if (!_isLowering)
        //    {
        //        _isLowering = true;
        //        Services.TimeService.StopTimer();
        //
        //
        //        player.transform.localScale = Vector3.one;
        //        player.transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
        //
        //        var _playerAnimator = player.GetComponentInChildren<PlayerAnimator_OLD>();
        //
        //        _audioSource.Play();
        //        StartCoroutine(DownPlayerPole(player, _playerAnimator));
        //        StartCoroutine(DownFlagPole(player, _playerAnimator));
        //
        //        Services.GameDataService.IsGoalReached = true;
        //    }
        //}
        //private IEnumerator DownFlagPole(PlayerController player, PlayerAnimator_OLD animator)
        //{
        //    Services.MusicService.Stop();
        //    Services.PlayerService.CanMove = false;
        //
        //    while (_flag.transform.position.y > transform.position.y + 0.5f)
        //    {
        //        _flag.transform.Translate(Vector3.down * Time.deltaTime * 9);
        //        yield return null;
        //    }
        //
        //    player.transform.Find("Visual").localScale = new Vector3(-1, 1, 1);
        //    player.transform.Translate(Vector3.right);
        //
        //    yield return new WaitForSeconds(0.4f);
        //    animator.IsInFlagPole = false;
        //    player.IsAutoWalk = true;
        //
        //    Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.VictoryTheme.Clip;
        //    Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.VictoryTheme.StartTime;
        //    Services.MusicService.Play();
        //    Services.PlayerService.CanMove = true;
        //}
        //private IEnumerator DownPlayerPole(PlayerController player, PlayerController animator)
        //{
        //    animator.IsInFlagPole = true;
        //    while (player.transform.position.y > transform.position.y)
        //    {
        //        player.transform.Translate(Vector3.down * Time.deltaTime * 9);
        //        yield return null;
        //    }
        //
        //    animator.IsInFlagBase = true;
        //}
        private int GetFlagPoints(PlayerController player)
        {
            var hitPoint = 1 - Mathf.InverseLerp(transform.position.y, transform.position.y + 10, player.transform.position.y);
            int index = Mathf.FloorToInt(Mathf.Clamp(hitPoint * _profile.Points.Length, 0, _profile.Points.Length));
            index = Mathf.Clamp(index, 0, _profile.Points.Length - 1);
            return _profile.Points[index];
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player)
        {
            ShowPoint(player);
            //LowerFlag(player);
        }
        #endregion
    }
}