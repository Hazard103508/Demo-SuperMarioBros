using UnityEngine;

namespace Mario.Game.Maps
{
    public class LeftMargin : MonoBehaviour
    {
        private float withMargin = 0.5f;

        void Update()
        {
            var cam = Camera.main;
            var downLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, cam.nearClipPlane));

            this.transform.position = new Vector3(downLeft.x - withMargin, this.transform.position.y, this.transform.position.z);
        }
    }
}