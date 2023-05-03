using UnityEngine;

namespace UnityShared.Demos
{
    public class SpriteKeepInBoundsDemo : MonoBehaviour
    {
        public GameObject ball;

        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow))
                ball.transform.Translate(Vector3.right * Time.deltaTime * 10);
            else if (Input.GetKey(KeyCode.LeftArrow))
                ball.transform.Translate(Vector3.left * Time.deltaTime * 10);

            if (Input.GetKey(KeyCode.UpArrow))
                ball.transform.Translate(Vector3.up * Time.deltaTime * 10);
            else if (Input.GetKey(KeyCode.DownArrow))
                ball.transform.Translate(Vector3.down * Time.deltaTime * 10);
        }
    }
}