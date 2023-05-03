using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventStringProfile), editorForChildClasses: true)]
    public class GameEventStringEditor : GameEventGenericEditor<string>
    {
    }
}