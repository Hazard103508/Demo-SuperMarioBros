using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerString), editorForChildClasses: true)]
    public class GameEventListenerStringEditor : GameEventListenerGenericEditor<string, GameEventStringProfile>
    {
    }
}

