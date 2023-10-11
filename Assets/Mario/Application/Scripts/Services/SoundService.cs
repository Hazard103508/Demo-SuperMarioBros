using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        private IPoolService _poolService;
        private AudioSource themeSong;

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
            themeSong = sound.GetComponent<AudioSource>();
            themeSong.time = initTime;
            themeSong.Play();
        }
        public void StopTheme()
        {
            if (themeSong != null)
            {
                themeSong.Stop();
                themeSong.gameObject.SetActive(false);
            }
        }
        public void Play(PooledSoundProfile soundProfile) => Play(soundProfile, Vector3.zero);
        public void Play(PooledSoundProfile soundProfile, Vector3 position)
        {
            var sound = _poolService.GetObjectFromPool(soundProfile, position);
            sound.GetComponent<AudioSource>().Play();
        }


    }
}
