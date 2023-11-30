using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Commons
{
    public class AudioPlayer : MonoBehaviour
    {
        public bool AllowPause;
        public bool DisableOnComplete;

        private IPauseService _pauseService;
        private AudioSource _audioSource;
        private bool _isPaused;

        #region Unity Methods
        private void Awake()
        {
            _pauseService = ServiceLocator.Current.Get<IPauseService>();
            _audioSource = GetComponent<AudioSource>();
            _pauseService.Paused += OnPaused;
            _pauseService.Resumed += OnResumed;
        }
        private void OnEnable()
        {
            _isPaused = false;
        }
        private void OnDestroy()
        {
            _pauseService.Paused -= OnPaused;
            _pauseService.Resumed -= OnResumed;
        }
        private void Update()
        {
            if (_isPaused)
                return;

            if (DisableOnComplete && !_audioSource.isPlaying)
                gameObject.SetActive(false);
        }
        #endregion

        #region Service Methods
        private void OnPaused()
        {
            if (!AllowPause)
                return;

            if (_audioSource.isPlaying)
            {
                _isPaused = true;
                _audioSource.Pause();
            }
        }
        private void OnResumed()
        {
            if (!AllowPause)
                return;

            if (_isPaused)
            {
                _isPaused = false;
                _audioSource.UnPause();
            }
        }
        #endregion
    }
}