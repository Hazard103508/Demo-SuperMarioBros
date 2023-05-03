using UnityEditor;
using UnityEngine;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventVector3Profile), editorForChildClasses: true)]
    public class GameEventVector3Editor : GameEventGenericEditor<Vector3>
    {
    }
}