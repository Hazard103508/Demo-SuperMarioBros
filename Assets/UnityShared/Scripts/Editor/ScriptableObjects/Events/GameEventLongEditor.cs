using UnityEditor;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventLongProfile), editorForChildClasses: true)]
    public class GameEventLongEditor : GameEventGenericEditor<long>
    {
    }
}