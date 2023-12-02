using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PauseService : MonoBehaviour, IPauseService
    {
        [SerializeField] private PooledSoundProfile _pauseSoundPoolReference;

        private IGameplayService _gameplayService;
        private ISoundService _soundService;

        public bool IsPaused { get; protected set; }

        public event Action Paused;
        public event Action Resumed;

        public void Initalize()
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        public void Dispose()
        {
        }
        public void Pause()
        {
            if (_gameplayService.State != GameplayService.GameState.Play)
                return;

            _soundService.Play(_pauseSoundPoolReference);
            Time.timeScale = 0;

            IsPaused = true;
            _gameplayService.State = GameplayService.GameState.Pause;
            Paused.Invoke();
        }
        public void Resume()
        {
            _soundService.Play(_pauseSoundPoolReference);
            Time.timeScale = 1;

            IsPaused = false;
            _gameplayService.State = GameplayService.GameState.Play;
            Resumed.Invoke();
        }
    }
}