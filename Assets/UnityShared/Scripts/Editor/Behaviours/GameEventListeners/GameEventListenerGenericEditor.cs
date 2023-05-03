using UnityEditor;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;
using UnityShared.ScriptableObjects.Events;

namespace UnityShared.Editor.Behaviours.GameEventListeners
{
    public class GameEventListenerGenericEditor<T, R> : UnityEditor.Editor where R : GameEventGenericProfile<T>
    {
        SerializedProperty response;

        private void OnEnable()
        {
            response = serializedObject.FindProperty("response");
        }
        public override void OnInspectorGUI()
        {
            var e = (GameEventListenerGeneric<T>)target;
            e.eventProfile = EditorGUILayout.ObjectField("Event profile", e.eventProfile, typeof(R), true) as R;

            serializedObject.Update();
            EditorGUILayout.PropertyField(response);
            serializedObject.ApplyModifiedProperties();

            GUI.enabled = Application.isPlaying;

            if (GUILayout.Button($"Raise"))
                e.eventProfile.Raise();
        }
    }
}

