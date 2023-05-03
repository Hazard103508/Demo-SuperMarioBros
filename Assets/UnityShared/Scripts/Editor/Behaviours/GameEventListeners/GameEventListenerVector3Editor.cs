using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerVector3), editorForChildClasses: true)]
    public class GameEventListenerVector3Editor : GameEventListenerGenericEditor<Vector3, GameEventVector3Profile>
    {
    }
}