using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Services
{
    public class SoundService : MonoBehaviour, ISoundService
    {
        private IPoolService _poolService;

        public void LoadService()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }

        public void Play(PooledSoundProfile soundProfile)
        {
            var aux = _poolService.GetObjectFromPool(soundProfile);
        }
    }
}
