using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public abstract class PoolFactory
    {
        protected IAddressablesService _addressablesService;

        protected Pool Pool { get; private set; }

        public PoolFactory()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
        }

        public virtual Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var obj = new GameObject($"Pool - {profile.name}");
            obj.transform.parent = parent;

            Pool = obj.AddComponent<Pool>();
            Pool.CollectionCheck = profile.CollectionCheck;
            Pool.DefaultCapacity = profile.DefaultCapacity;
            Pool.MaxSize = profile.MaxSize;

            return Pool;
        }
    }
}