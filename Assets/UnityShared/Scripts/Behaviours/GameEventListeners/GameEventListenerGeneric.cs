using UnityEngine;
using UnityEngine.Events;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Behaviours.GameEventListeners
{
    [DisallowMultipleComponent]
    public class GameEventListenerGeneric<T> : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEventGenericProfile<T> eventProfile;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent<T> response;

        private void OnEnable() => eventProfile?.RegisterListener(this);

        private void OnDisable() => eventProfile?.UnregisterListener(this);

        public void Raise(T arg)
        {
            if (eventProfile != null)
            {
                eventProfile.value = arg;
                response.Invoke(arg);
            }
        }
    }
}