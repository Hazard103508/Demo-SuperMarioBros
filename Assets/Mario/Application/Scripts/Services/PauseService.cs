using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Services
{
    public class PauseService : MonoBehaviour, IPauseService
    {
        [SerializeField] private PooledSoundProfile _pauseSoundPoolReference;
        
        private ISoundService _soundService;

        public bool IsPaused { get; protected set; }

        public void Initalize()
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        public void Dispose()
        {
        }
        public void Pause()
        {
            _soundService.Play(_pauseSoundPoolReference);
            Time.timeScale = 0;
            IsPaused = true;
        }
        public void Resume()
        {
            _soundService.Play(_pauseSoundPoolReference);
            Time.timeScale = 1;
            IsPaused = false;
        }
    }
}