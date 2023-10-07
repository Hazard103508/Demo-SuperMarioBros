using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        private IPoolService _poolService;

        public void Initalize()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }

        public void Play(PooledSoundProfile soundProfile) => Play(soundProfile, Vector3.zero);
        public void Play(PooledSoundProfile soundProfile, Vector3 position)
        {
            var sound = _poolService.GetObjectFromPool(soundProfile, position);
            sound.GetComponent<AudioSource>().Play();
        }
    }
}
