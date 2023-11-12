using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactoryObject : PoolFactory
    {
        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var pool = base.CreatePool(profile, parent);
            pool.PrefabReference = _addressablesService.GetAssetReference<GameObject>(profile.name);

            pool.Load();
            return pool;
        }
    }
}