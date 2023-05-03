using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.PropertyAttributes;

namespace UnityShared.Behaviours.GameActions
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class GameActionCollision2D : MonoBehaviour
    {
        [TagSelector] public string targetTag;

        public UnityEvent<Collision2D> callback;

        protected void OnDetection(Collision2D other)
        {
            if (other.gameObject == null)
                return;

            if (!string.IsNullOrEmpty(targetTag) && !other.gameObject.CompareTag(targetTag))
                return;

            callback.Invoke(other);
        }
    }
}