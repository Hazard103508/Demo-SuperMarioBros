using UnityEngine;
using UnityEngine.UI;
using UnityShared.Behaviours.Handlers;

namespace UnityShared.Demos.Handlers
{
    public class PauseDemo : MonoBehaviour
    {
        public Image image;
        public PauseHandler PauseHandler;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (PauseHandler.IsPaused)
                    PauseHandler.Resume();
                else
                    PauseHandler.Pause();
            }

            image.transform.Rotate(Vector3.forward * 90 * Time.deltaTime);
        }
    }
}