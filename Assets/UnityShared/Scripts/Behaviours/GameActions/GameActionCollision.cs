using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.PropertyAttributes;

namespace UnityShared.Behaviours.GameActions
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class GameActionCollision : MonoBehaviour
    {
        [TagSelector] public string targetTag;

        public UnityEvent<Collision> callback;

        protected void OnDetection(Collision other)
        {
            if (other.gameObject == null)
                return;

            if (!string.IsNullOrEmpty(targetTag) && !other.gameObject.CompareTag(targetTag))
                return;

            callback.Invoke(other);
        }
    }
}