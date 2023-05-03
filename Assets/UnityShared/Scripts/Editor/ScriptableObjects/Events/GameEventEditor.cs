using UnityEditor;
using UnityEngine;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(GameEventProfile), editorForChildClasses: true)]
    public class GameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEventProfile e = target as GameEventProfile;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}