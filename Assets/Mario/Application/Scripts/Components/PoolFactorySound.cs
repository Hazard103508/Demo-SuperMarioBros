using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactorySound : PoolFactory
    {
        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var pool = base.CreatePool(profile, parent);
            //PooledSoundProfile soundProfile = (PooledSoundProfile)profile;

            //var objRef = Instantiate(_addressablesService.GetAssetReference<GameObject>(profile.Reference));
            //objRef.transform.parent = null;
            //var audioSource = objRef.GetComponent<AudioSource>();
            //audioSource.clip = profile.Clip;
            //
            //pool.PrefabReference = objRef;
            //LoadItemPool(pool, profile);

            //pool.Load();
            return pool;
        }
    }
}