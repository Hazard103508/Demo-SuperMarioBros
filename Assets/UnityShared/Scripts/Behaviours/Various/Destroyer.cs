using UnityEngine;
using UnityEngine.Events;
using UnityShared.Enums;

namespace UnityShared.Behaviours.Various
{
    public class Destroyer : MonoBehaviour
    {
        public DestructionType destructionType;

        public void Destroy()
        {
            UnityAction destroy = destructionType switch
            {
                DestructionType.GAMEOBJECT => () => Object.Destroy(this.gameObject),
                DestructionType.COMPONENT => () => Component.Destroy(this),
                DestructionType.ROOT => () => Object.Destroy(this.transform.root.gameObject),
                _ => throw new System.NotImplementedException(),
            };

            destroy?.Invoke();
        }
    }
}