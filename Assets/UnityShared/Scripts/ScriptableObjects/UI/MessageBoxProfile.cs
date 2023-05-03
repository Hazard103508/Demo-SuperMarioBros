using UnityEngine;

namespace UnityShared.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "MessageBoxProfile", menuName = "ScriptableObjects/UI/MessageBox", order = 99)]
    public class MessageBoxProfile : ScriptableObject
    {
        public Sprite icon;
        public string message;
    }
}