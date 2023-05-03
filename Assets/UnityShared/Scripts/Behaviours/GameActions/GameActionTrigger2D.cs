using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.PropertyAttributes;

namespace UnityShared.Behaviours.GameActions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class GameActionTrigger2D : MonoBehaviour
    {
        [TagSelector] public string targetTag;
        public UnityEvent<Collider2D> callback;

        protected void OnDetection(Collider2D other)
        {
            if (other.gameObject == null)
                return;

            if (!string.IsNullOrEmpty(targetTag) && !other.gameObject.CompareTag(targetTag))
                return;

            callback.Invoke(other);
        }
    }
}