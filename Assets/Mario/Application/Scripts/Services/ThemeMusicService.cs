using Mario.Application.Interfaces;
using UnityEngine;

namespace Mario.Application.Services
{
    public class ThemeMusicService : MonoBehaviour, IThemeMusicService
    {
        #region Objects
        [SerializeField] private AudioSource _audioSource;
        #endregion

        #region Properties
        public AudioClip Clip
        {
            get => _audioSource.clip;
            set
            {
                Stop();
                _audioSource.clip = value;
            }
        }
        public float Time { get => _audioSource.time; set => _audioSource.time = value; }
        #endregion

        #region Public Methods
        public void LoadService()
        {
        }
        public void Play() => _audioSource.Play();
        public void Stop()
        {
            if (_audioSource != null)
                _audioSource.Stop();
        }
        #endregion
    }
}