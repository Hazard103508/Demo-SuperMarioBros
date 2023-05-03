using UnityEngine;

namespace UnityShared.Behaviours.Controllers.Cameras
{
    public class IsometricCameraFollowerController : MonoBehaviour
    {
        public Transform target;
        public float smoothing = 5f;
        Vector3 offset;

        void Start()
        {
            offset = transform.position - target.position;
        }

        void LateUpdate()
        {
            Vector3 targetCamPos = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        }
    }
}