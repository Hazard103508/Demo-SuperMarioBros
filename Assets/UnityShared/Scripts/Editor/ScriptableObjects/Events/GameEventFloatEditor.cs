using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventFloatProfile), editorForChildClasses: true)]
    public class GameEventFloatEditor : GameEventGenericEditor<float>
    {
    }
}