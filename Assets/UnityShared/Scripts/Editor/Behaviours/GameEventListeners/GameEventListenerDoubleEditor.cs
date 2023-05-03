using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerDouble), editorForChildClasses: true)]
    public class GameEventListenerDoubleEditor : GameEventListenerGenericEditor<double, GameEventDoubleProfile>
    {
    }
}

