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
            // OBTENER EL POOL GROUP SOLAMENTE
            // VALIDAR SI EXISTE EL OBJETO EN EL POOL
            // CREAR EL OBJETO CON UN PREFAB TEMPLATE DEL SOUND-SERVICE
            // AGREGAR OBJETO AL POOL
            _poolService.GetObjectFromPool(soundProfile);
        }
        public void Play(PooledSoundProfile soundProfile, Vector3 position) => _poolService.GetObjectFromPool(soundProfile, position);
    }
}
