using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerLong), editorForChildClasses: true)]
    public class GameEventListenerLongEditor : GameEventListenerGenericEditor<long, GameEventLongProfile>
    {
    }
}

