using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerBoolean), editorForChildClasses: true)]
    public class GameEventListenerBooleanEditor : GameEventListenerGenericEditor<bool, GameEventBooleanProfile>
    {
    }
}

