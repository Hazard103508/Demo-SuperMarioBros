using UnityEngine;
using UnityEngine.Events;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Behaviours.GameEventListeners
{
    public class GameEventListener : MonoBehaviour
    {
        [Tooltip("Event to register with.")]
        public GameEventProfile eventProfile;

        [Tooltip("Response to invoke when Event is raised.")]
        public UnityEvent response;

        private void OnEnable() => eventProfile.RegisterListener(this);

        private void OnDisable() => eventProfile.UnregisterListener(this);

        public void Raise() => response.Invoke();
    }
}