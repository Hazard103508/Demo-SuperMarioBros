using Mario.Game.Commons;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactorySound : PoolFactory
    {
        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var pool = base.CreatePool(profile, parent);
            pool.OnCreate = OnCreate;

            pool.Load();
            return pool;
        }
        private void OnCreate(Pool pool, GameObject obj)
        {
            var _profile = (PooledSoundProfile)pool.Profile;

            var audioSource = obj.AddComponent<AudioSource>();
            audioSource.clip = _addressablesService.GetAssetReference<AudioClip>(_profile.name, _profile.Reference);
            audioSource.volume = _profile.Volume;

            if (_profile.DisableOnComplete)
                obj.AddComponent<DisableOnAudioCompleted>();
        }
    }
}