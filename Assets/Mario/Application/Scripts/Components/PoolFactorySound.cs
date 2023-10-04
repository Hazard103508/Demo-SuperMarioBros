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

            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.Reference);
            if (pool.PrefabReference == null)
                Debug.LogError($"Missing sound asset reference: {profile.name}");

            pool.Load();
            return pool;
        }
        private void OnCreate(Pool pool, GameObject obj) 
        {
            var _profile = (PooledSoundProfile)pool.Profile;
            var audioSource = obj.GetComponent<AudioSource>();
            audioSource.clip = _addressablesService.GetAssetReference<AudioClip>(_profile.Clip);
        }
    }
}