using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Application.Components
{
    public abstract class PoolFactory
    {
        protected IAddressablesService _addressablesService;

        public PoolFactory()
        {
            _addressablesService = ServiceLocator.Current.Get<IAddressablesService>();
        }

        public virtual Pool CreatePool(PooledBaseProfile profile, Transform parent)
        {
            var obj = new GameObject($"Pool - {profile.name}");
            obj.transform.parent = parent;

            var pool = obj.AddComponent<Pool>();
            pool.Profile = profile;

            if (profile.Reference == null)
                Debug.LogError($"Missing asset reference: {profile.name}");

            return pool;
        }
    }
}