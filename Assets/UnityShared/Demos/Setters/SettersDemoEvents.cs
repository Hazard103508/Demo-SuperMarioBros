using UnityEngine;

namespace UnityShared.Demos
{
    public class SettersDemoEvents : MonoBehaviour
    {
        public delegate void TransferSprite(Sprite value);
        public delegate void TransferString(string value);
        public delegate void TransferVector3(Vector3 value);

        public static event TransferSprite SetRawImageSprite;
        public static event TransferSprite SetImageSprite;
        public static event TransferString SetTextMeshPro;
        public static event TransferVector3 SetPosition;
        public static event TransferVector3 SetRotation;

        public static void Invoke_SetRawImageSprite(Sprite sprite) => SetRawImageSprite?.Invoke(sprite);
        public static void Invoke_SetImageSpriteB(Sprite sprite) => SetImageSprite?.Invoke(sprite);
        public static void Invoke_SetTextMeshPro(string text) => SetTextMeshPro?.Invoke(text);
        public static void Invoke_SetPosition(Vector3 position) => SetPosition?.Invoke(position);
        public static void Invoke_SetRotation(Vector3 rotation) => SetRotation?.Invoke(rotation);
    }
}