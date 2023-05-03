using UnityEngine;
using UnityEngine.UI;

namespace UnityShared.Demos.Controllers.Cameras
{
    public class FollowerDemo : MonoBehaviour
    {
        public Text labelDemo;
        public GameObject target;

        private void LateUpdate()
        {
            labelDemo.text = $"{System.Math.Round(target.transform.position.x, 2)};{System.Math.Round(target.transform.position.y, 2)}";
        }
    }
}