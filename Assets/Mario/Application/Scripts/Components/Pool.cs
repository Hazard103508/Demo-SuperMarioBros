using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Mario.Application.Components
{
    public class Pool : MonoBehaviour
    {
        #region Objects
        private IObjectPool<PooledObject> objectPool;
        private Vector3 _nextObjectPosition;
        #endregion

        #region Properties
        public PooledBaseProfile Profile { get; set; }
        public GameObject PrefabReference { get; set; }
        public Action<Pool, GameObject> OnCreate { get; set; }
        #endregion

        #region Public Methods
        public void Load()
        {
            objectPool = new ObjectPool<PooledObject>(
                CreateInstance,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyPooledObject,
                true,
                Profile.DefaultCapacity,
                Profile.MaxSize);
        }
        public PooledObject Get(Vector3 position)
        {
            _nextObjectPosition = position;
            return objectPool.Get();
        }
        #endregion

        #region Private Methods
        private PooledObject CreateInstance()
        {
            GameObject obj = PrefabReference != null ? Instantiate(PrefabReference) : new GameObject(Profile.name);
            obj.transform.SetParent(transform);
            obj.transform.position = _nextObjectPosition;
            obj.transform.localScale = Vector3.one;

            PooledObject pooledObject = obj.AddComponent<PooledObject>();
            pooledObject.ObjectPool = objectPool;
            OnCreate?.Invoke(this, obj);

            return pooledObject;
        }
        private void OnReleaseToPool(PooledObject pooledObject) => pooledObject.gameObject.SetActive(false);
        private void OnGetFromPool(PooledObject pooledObject)
        {
            pooledObject.gameObject.transform.position = _nextObjectPosition;
            pooledObject.gameObject.SetActive(true);
        }
        private void OnDestroyPooledObject(PooledObject pooledObject) => Destroy(pooledObject.gameObject);
        #endregion
    }
}