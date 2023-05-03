using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListenerGameObject), editorForChildClasses: true)]
    public class GameEventListenerGameObjectEditor : GameEventListenerGenericEditor<GameObject, GameEventGameObjectProfile>
    {
    }
}