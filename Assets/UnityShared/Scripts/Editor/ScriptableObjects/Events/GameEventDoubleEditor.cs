using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventDoubleProfile), editorForChildClasses: true)]
    public class GameEventDoubleEditor : GameEventGenericEditor<double>
    {
    }
}