using UnityEngine;

namespace UnityShared.Demos.UI
{
    public class ItemCollectorViewer : MonoBehaviour
    {

        public UnityShared.Behaviours.UI.ItemCollectorViewer viewer;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                viewer.Run();
        }
    }
}