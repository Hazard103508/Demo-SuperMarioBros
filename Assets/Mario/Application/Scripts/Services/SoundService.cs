using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        private IPoolService _poolService;
        private AudioSource _themeSong;
        private AudioSource _soundSong;

        public void Initalize()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        public void Dispose()
        {
        }

        public void PlayTheme(PooledSoundProfile soundProfile) => PlayTheme(soundProfile, 0);
        public void PlayTheme(PooledSoundProfile soundProfile, float initTime)
        {
            StopTheme();

            var sound = _poolService.GetObjectFromPool(soundProfile);
            _themeSong = sound.GetComponent<AudioSource>();
            _themeSong.time = initTime;
            _themeSong.Play();
        }
        public void StopTheme()
        {
            if (_themeSong != null)
            {
                _themeSong.Stop();
                _themeSong.gameObject.SetActive(false);
            }
        }
        public void Play(PooledSoundProfile soundProfile) => Play(soundProfile, Vector3.zero);
        public void Play(PooledSoundProfile soundProfile, Vector3 position)
        {
            var sound = _poolService.GetObjectFromPool(soundProfile, position);
            _soundSong = sound.GetComponent<AudioSource>();
            _soundSong.Play();
        }
        public void Stop() => _soundSong?.Stop();
    }
}
