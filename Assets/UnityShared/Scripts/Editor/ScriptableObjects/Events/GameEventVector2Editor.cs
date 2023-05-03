using UnityEditor;
using UnityEngine;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventVector2Profile), editorForChildClasses: true)]
    public class GameEventVector2Editor : GameEventGenericEditor<Vector2>
    {
    }
}