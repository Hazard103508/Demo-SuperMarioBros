using UnityEditor;
using UnityEngine;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventGameObjectProfile), editorForChildClasses: true)]
    public class GameEventGameObjectEditor : GameEventGenericEditor<GameObject>
    {
    }
}