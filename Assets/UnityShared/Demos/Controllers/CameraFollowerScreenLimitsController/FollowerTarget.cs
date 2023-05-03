using UnityEngine;

namespace UnityShared.Demos.Controllers.Cameras
{
    public class FollowerTarget : MonoBehaviour
    {
        public float speed;

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, Input.GetAxis("Vertical") * speed * Time.deltaTime, 0);
        }
    }
}