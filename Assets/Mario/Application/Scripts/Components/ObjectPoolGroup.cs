using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mario.Application.Components
{
    public class ObjectPoolGroup : MonoBehaviour
    {
        #region Objects
        public string Type;
        private Queue<GameObject> _poolReady;
        private Queue<GameObject> _poolBussy;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _poolReady = new Queue<GameObject>();
            _poolBussy = new Queue<GameObject>();
        }
        #endregion

        #region Public Methods
        public GameObject GetPoolObject()
        {
            if (!_poolReady.Any())
                IncreasePool();

            var objInstance = _poolReady.Dequeue();
            objInstance.SetActive(true);
            _poolBussy.Enqueue(objInstance);
            return objInstance;
        }
        #endregion

        #region Private Methods
        private void IncreasePool()
        {
            var poolItem = Services.Services.GameDataService.CurrentMapProfile.ObjectsPool.PoolObjectsDic[this.Type];
            var itemReference = Services.Services.AddressablesService.GetAssetReference(poolItem.Reference);

            var instance = Instantiate(itemReference, transform);
            var poolObject = instance.GetComponent<ObjectPool>();
            poolObject.TaskCompleted.AddListener(() => _poolReady.Enqueue(instance));
            _poolReady.Enqueue(instance);
        }
        #endregion
    }
}