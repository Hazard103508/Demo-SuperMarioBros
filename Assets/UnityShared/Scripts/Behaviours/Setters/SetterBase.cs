using UnityEngine;

namespace UnityShared.Behaviours.Setters
{
    public abstract class SetterBase<T, R> : MonoBehaviour where T : class
    {
        [SerializeField] protected T containerComponent;

        private void Awake()
        {
            containerComponent = OnGetComponent();

            if (containerComponent == null)
                throw new System.NullReferenceException($"This component expects to get an {typeof(T).Name} component.");
        }

        protected virtual T OnGetComponent() => containerComponent ?? GetComponent<T>();
        protected abstract void OnSetValue(R value);
    }
}

