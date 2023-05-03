using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Various
{
    public class ObjectCreator : MonoBehaviour
    {
        public GameObject prefab;
        public Transform parent;

        public UnityEvent<GameObject> onObjectCreated;

        public void Create()
        {
            var obj = Instantiate(prefab, parent);
            onObjectCreated.Invoke(obj);
        }
    }
}