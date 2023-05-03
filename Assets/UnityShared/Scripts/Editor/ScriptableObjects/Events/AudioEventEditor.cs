using UnityEditor;
using UnityEngine;
using UnityShared.ScriptableObjects.Audio;

namespace UnityShared.Editor.ScriptableObjects.Events
{
    [CustomEditor(typeof(AudioProfile), editorForChildClasses: true)]
    public class AudioEventEditor : UnityEditor.Editor
    {
        [SerializeField] private AudioSource _previewer;

        public void OnEnable() => _previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();

        public void OnDisable() => DestroyImmediate(_previewer.gameObject);

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((AudioProfile)target).Play(_previewer);
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}