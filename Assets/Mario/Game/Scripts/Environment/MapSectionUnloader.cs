using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapSectionUnloader : MonoBehaviour
    {
        public int Width;

        void Update()
        {
            var viewportPoint = Camera.main.ViewportToWorldPoint(Vector3.zero);
            
            var position = transform.position + Vector3.right * this.Width;
            if (viewportPoint.x > position.x)
                Destroy(gameObject);
        }
    }
}