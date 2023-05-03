using UnityEngine;
using UnityShared.ScriptableObjects.GameObjects;

namespace UnityShared.Behaviours.Various
{
    public class Breakable : MonoBehaviour
    {
        [SerializeField] private BreakableProfile profile;
        [SerializeField] private bool _broken;

        private void OnCollisionEnter(Collision collision)
        {
            if (_broken)
                return;

            if (collision.relativeVelocity.magnitude >= profile.breakForce)
            {
                _broken = true;
                var replacement = Instantiate(profile.replacement, transform.position, transform.rotation);

                var rbs = replacement.GetComponentsInChildren<Rigidbody>();
                foreach (var rb in rbs)
                    rb.AddExplosionForce(collision.relativeVelocity.magnitude * profile.collisionMultiplier, collision.contacts[0].point, 2);

                gameObject.SetActive(false);
            }
        }
    }
}