using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventIntProfile), editorForChildClasses: true)]
    public class GameEventIntEditor : GameEventGenericEditor<int>
    {
    }
}