using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventBooleanProfile), editorForChildClasses: true)]
    public class GameEventBooleanEditor : GameEventGenericEditor<bool>
    {
    }
}