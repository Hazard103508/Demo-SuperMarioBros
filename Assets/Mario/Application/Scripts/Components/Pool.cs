using UnityEngine;
using UnityEngine.Pool;

namespace Mario.Application.Components
{
    public class Pool : MonoBehaviour
    {
        #region Objects
        private IObjectPool<PooledObject> objectPool;
        #endregion

        #region Properties
        public GameObject PrefabReference { get; set; }
        public bool CollectionCheck { get; set; }
        public int DefaultCapacity { get; set; }
        public int MaxSize { get; set; }
        #endregion

        #region Public Methods
        public void Load()
        {
            objectPool = new ObjectPool<PooledObject>(
                CreateInstance,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                CollectionCheck,
                DefaultCapacity,
                MaxSize);
        }
        public PooledObject Get() => objectPool.Get();
        #endregion

        #region Private Methods
        private PooledObject CreateInstance()
        {
            var obj = Instantiate(PrefabReference, transform);
            PooledObject pooledObject = obj.AddComponent<PooledObject>();
            pooledObject.ObjectPool = objectPool;
            return pooledObject;
        }
        private void OnReleaseToPool(PooledObject pooledObject) => pooledObject.gameObject.SetActive(false);
        private void OnGetFromPool(PooledObject pooledObject) => pooledObject.gameObject.SetActive(true);
        private void OnDestroyPooledObject(PooledObject pooledObject) => Destroy(pooledObject.gameObject);
        #endregion
    }
}