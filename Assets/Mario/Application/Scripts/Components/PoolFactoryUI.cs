using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public class PoolFactoryUI : PoolFactory
    {
        public override Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var pool = base.CreatePool(profile, parent);

            return pool;
        }
    }
}