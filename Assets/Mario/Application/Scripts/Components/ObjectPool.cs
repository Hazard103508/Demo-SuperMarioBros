using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Components
{
    public class ObjectPool : MonoBehaviour
    {
        [HideInInspector] public UnityEvent TaskCompleted;
        private ITakenFromPool takenFromPool;

        #region Unity Methods
        protected virtual void Awake()
        {
            takenFromPool = GetComponent<ITakenFromPool>();
            TaskCompleted = new UnityEvent();
        }
        protected virtual void OnEnable()
        {
            takenFromPool?.OnTakenFromPool();
        }
        protected virtual void OnDisable()
        {
            TaskCompleted.Invoke();
        }
        #endregion
    }
}