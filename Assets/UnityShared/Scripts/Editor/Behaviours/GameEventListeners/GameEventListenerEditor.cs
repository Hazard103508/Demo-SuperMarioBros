using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    [CustomEditor(typeof(GameEventListener), editorForChildClasses: true)]
    public class GameEventListenerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = Application.isPlaying;

            GameEventListener e = target as GameEventListener;
            if (GUILayout.Button("Raise"))
                e.eventProfile.Raise();
        }
    }
}

