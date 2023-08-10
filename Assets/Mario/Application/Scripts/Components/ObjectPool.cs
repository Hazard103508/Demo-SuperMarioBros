using UnityEngine;
using UnityEngine.Pool;

namespace Mario.Application.Components
{
    public class ObjectPool : MonoBehaviour
    {
        [HideInInspector] public GameObject PrefabReference;

        private IObjectPool<PooledObject> objectPool;
        
        // -- REVISAR ESTA MIERDA ---
        [SerializeField] private bool collectionCheck = true;
        [SerializeField] private int defaultCapacity = 20;
        [SerializeField] private int maxSize = 100;

        private void Awake()
        {
            objectPool = new ObjectPool<PooledObject>(
                CreateInstance,
                OnGetFromPool, 
                OnReleaseToPool, 
                OnDestroyPooledObject,
                collectionCheck, 
                defaultCapacity, 
                maxSize);

        }
        public PooledObject Get() => objectPool.Get();
        private PooledObject CreateInstance()
        {
            var obj = Instantiate(PrefabReference, transform);
            PooledObject pooledObject = obj.AddComponent<PooledObject>();
            pooledObject.ObjectPool = objectPool;
            return pooledObject;
        }
        private void OnReleaseToPool(PooledObject pooledObject)
        {
            pooledObject.gameObject.SetActive(false);
        }
        private void OnGetFromPool(PooledObject pooledObject)
        {
            pooledObject.gameObject.SetActive(true);
        }
        private void OnDestroyPooledObject(PooledObject pooledObject)
        {
            Destroy(pooledObject.gameObject);
        }
    }
}