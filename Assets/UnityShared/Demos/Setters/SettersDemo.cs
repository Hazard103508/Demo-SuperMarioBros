using UnityEngine;

namespace UnityShared.Demos
{
    public class SettersDemo : MonoBehaviour
    {
        public Sprite spriteDemo;

        public void onButtonSetRawImageSprite_Click() => SettersDemoEvents.Invoke_SetRawImageSprite(spriteDemo);
        public void onButtonSetImageSprite_Click() => SettersDemoEvents.Invoke_SetImageSpriteB(spriteDemo);
        public void onButtonSetTextMeshPro_Click() => SettersDemoEvents.Invoke_SetTextMeshPro("Hola mundo!");
        public void onButtonSetPosition_Click() => SettersDemoEvents.Invoke_SetPosition(Vector3.right);
        public void onButtonSetRotation_Click() => SettersDemoEvents.Invoke_SetRotation(Vector3.up * 45f);
    }
}