using Mario.Commons.Testing;
using UnityEditor;
using UnityEngine;

namespace Mario.Commons.Editor
{
    [CustomEditor(typeof(GameEventListener), editorForChildClasses: true)]
    public class GameEventListenerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUI.enabled = UnityEngine.Application.isPlaying;

            GameEventListener e = target as GameEventListener;
            if (GUILayout.Button("Raise"))
                e.eventProfile.Raise();
        }
    }
}

