using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerInt), editorForChildClasses: true)]
    public class GameEventListenerIntEditor : GameEventListenerGenericEditor<int, GameEventIntProfile>
    {
    }
}

