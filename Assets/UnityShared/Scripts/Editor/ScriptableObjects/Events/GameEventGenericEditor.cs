using UnityEngine;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    public class GameEventGenericEditor<T> : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            var e = target as GameEventGenericProfile<T>;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}