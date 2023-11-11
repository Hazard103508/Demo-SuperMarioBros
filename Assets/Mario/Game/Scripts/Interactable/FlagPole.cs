using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Interactable;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, IHittableByPlayerFromLeft
    {
        #region Objects
        private IScoreService _scoreService;
        private ITimeService _timeService;
        private ISoundService _soundService;
        private IPlayerService _playerService;
        private IGameplayService _gameplayService;

        [SerializeField] private FlagPoleProfile _profile;
        [SerializeField] private GameObject _flag;
        [SerializeField] private FlagScoreLabel _scoreLabel;
        private bool _isLowering;
        private bool _isPlayerDown;
        private bool _isFlagDown;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Private Methods
        private void TakeFlag(PlayerController player)
        {
            if (!_isLowering)
            {
                _isLowering = true;

                int point = GetFlagPoints(player);
                _scoreService.Add(point);
                _scoreLabel.ShowPoint(point);
                _timeService.FreezeTimer();
                _soundService.StopTheme();
                _soundService.Play(_profile.FlagSoundFXPoolReference);

                StartCoroutine(DownFlagPole());
                StartCoroutine(DownPlayerPole(player));
                StartCoroutine(WalkToHouse(player));
            }
        }
        private IEnumerator DownPlayerPole(PlayerController player)
        {
            player.StateMachine.TransitionTo(player.StateMachine.CurrentMode.StateFlag);
            player.transform.localScale = Vector3.one;
            player.transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);

            while (player.transform.position.y > transform.position.y)
            {
                player.transform.Translate(9 * Time.deltaTime * Vector3.down);
                yield return null;
            }

            player.Animator.speed = 0;
            player.transform.position = new Vector3(player.transform.position.x, transform.position.y);
            player.Renderer.flipX = true;
            player.transform.Translate(Vector3.right);

            _isPlayerDown = true;
        }
        private IEnumerator DownFlagPole()
        {
            while (_flag.transform.position.y > transform.position.y + 0.5f)
            {
                _flag.transform.Translate(9 * Time.deltaTime * Vector3.down);
                yield return null;
            }

            _isFlagDown = true;
        }
        private IEnumerator WalkToHouse(PlayerController player)
        {
            yield return new WaitUntil(() => _isPlayerDown && _isFlagDown);

            player.StateMachine.TransitionTo(player.StateMachine.CurrentMode.StateRun);
            _playerService.EnableAutoWalk(true);
            player.Movable.SetJumpForce(0);

            yield return new WaitForSeconds(.3f);
            _gameplayService.SetFlagReached();
        }
        private int GetFlagPoints(PlayerController player)
        {
            var hitPoint = 1 - Mathf.InverseLerp(transform.position.y, transform.position.y + 10, player.transform.position.y);
            int index = Mathf.FloorToInt(Mathf.Clamp(hitPoint * _profile.Points.Length, 0, _profile.Points.Length));
            index = Mathf.Clamp(index, 0, _profile.Points.Length - 1);
            return _profile.Points[index];
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player) => TakeFlag(player);
        #endregion
    }
}