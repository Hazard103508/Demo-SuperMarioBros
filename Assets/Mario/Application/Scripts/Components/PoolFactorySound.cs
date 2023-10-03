using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactorySound : PoolFactory
    {
        private PooledSoundProfile _profile;

        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            _profile = (PooledSoundProfile)profile;

            var pool = base.CreatePool(profile, parent);
            pool.OnCreate = OnCreate;

            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
            if (pool.PrefabReference == null)
                Debug.LogError($"Missing sound asset reference: {profile.name}");

            pool.Load();
            return pool;
        }
        private void OnCreate(GameObject obj) 
        {
            var audioSource = obj.GetComponent<AudioSource>();
            audioSource.clip = _addressablesService.GetAssetReference<AudioClip>(_profile.Clip);
        }
    }
}