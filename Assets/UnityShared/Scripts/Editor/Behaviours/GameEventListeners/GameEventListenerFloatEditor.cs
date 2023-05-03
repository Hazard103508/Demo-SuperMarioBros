using UnityEditor;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerFloat), editorForChildClasses: true)]
    public class GameEventListenerFloatEditor : GameEventListenerGenericEditor<float, GameEventFloatProfile>
    {
    }
}

