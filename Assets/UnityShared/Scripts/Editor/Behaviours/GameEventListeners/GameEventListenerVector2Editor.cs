using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerVector2), editorForChildClasses: true)]
    public class GameEventListenerVector2Editor : GameEventListenerGenericEditor<Vector2, GameEventVector2Profile>
    {
    }
}