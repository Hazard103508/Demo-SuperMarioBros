using UnityEngine;
using UnityEngine.Pool;

namespace Mario.Application.Components
{
    public class PooledObject : MonoBehaviour
    {
        private IObjectPool<PooledObject> objectPool;
        public IObjectPool<PooledObject> ObjectPool { set => objectPool = value; }

        private void OnDisable() => objectPool.Release(this);
    }
}