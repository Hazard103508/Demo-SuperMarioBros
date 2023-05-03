using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Triggers
{
    public class VisibleChangeTrigger : MonoBehaviour
    {
        public UnityEvent<GameObject> onBecameVisible;
        public UnityEvent<GameObject> onBecameInvisible;

        private void OnBecameVisible() => onBecameVisible.Invoke(this.gameObject);
        private void OnBecameInvisible() => onBecameInvisible.Invoke(this.gameObject);
    }
}