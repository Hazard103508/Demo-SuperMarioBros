using UnityEditor;
using UnityShared.Behaviours.Testing;

namespace UnityShared.Editor.Behaviours.Triggers
{
    [CustomEditor(typeof(DebugLog), true)]
    public class DebugLogEditor : UnityEditor.Editor
    {
        SerializedProperty m_UseCustomColor;
        SerializedProperty m_LogColor;

        private void OnEnable()
        {
            m_UseCustomColor = serializedObject.FindProperty("useCustomColor");
            m_LogColor = serializedObject.FindProperty("logColor");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_UseCustomColor);
            if (m_UseCustomColor.boolValue)
                EditorGUILayout.PropertyField(m_LogColor);

            serializedObject.ApplyModifiedProperties();
        }
    }
}