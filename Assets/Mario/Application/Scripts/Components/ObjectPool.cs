using Mario.Application.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Components
{
    public class ObjectPool : MonoBehaviour
    {
        [HideInInspector] public UnityEvent TaskCompleted;

        #region Unity Methods
        protected virtual void Awake()
        {
            TaskCompleted = new UnityEvent();
        }
        protected virtual void OnDisable()
        {
            TaskCompleted.Invoke();
        }
        #endregion
    }
}