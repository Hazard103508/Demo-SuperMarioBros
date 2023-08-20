using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace RossoGame.Application.Editor
{
    [InitializeOnLoad]
    public class GameStartButton
    {
        const string ICONS_PATH = "Assets/Mario/Application/Sprites/Editor/";
        const string editScene = "_BORRAR";
        const string startScene = "Init";

        static GameStartButton()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
        }

        static void OnToolbarGUI()
        {
            GUILayout.FlexibleSpace();

            GUIContent startButtonContent = CreateIconContent("Joystick.png", "Start Game");
            if (GUILayout.Button(startButtonContent, ToolbarStyles.commandButtonStyle))
            {
                SceneHelper.StartScene(startScene, true);
            }

            GUIContent editButtonContent = CreateIconContent("Edit.png", "Edit Scene");
            if (GUILayout.Button(editButtonContent, ToolbarStyles.commandButtonStyle))
            {
                SceneHelper.StartScene(editScene, false);
            }
        }

        public static GUIContent CreateIconContent(string localTex, string tooltip)
        {
            var tex = LoadTexture(localTex);
            return new GUIContent(tex, tooltip);
        }

        public static Texture2D LoadTexture(string path)
        {
            return (Texture2D)EditorGUIUtility.Load(ICONS_PATH + path);
        }
    }
    static class ToolbarStyles
    {
        public static readonly GUIStyle commandButtonStyle;

        static ToolbarStyles()
        {
            commandButtonStyle = new GUIStyle("Command")
            {
                fontSize = 16,
                alignment = TextAnchor.MiddleCenter,
                imagePosition = ImagePosition.ImageAbove,
                fontStyle = FontStyle.Bold,
            };
        }
    }

    static class SceneHelper
    {
        static string sceneToOpen;
        static bool isPlaying;

        public static void StartScene(string sceneName, bool play)
        {
            isPlaying = play;

            if (EditorApplication.isPlaying)
                EditorApplication.isPlaying = false;

            sceneToOpen = sceneName;
            EditorApplication.update += OnUpdate;
        }

        static void OnUpdate()
        {
            if (sceneToOpen == null ||
                EditorApplication.isPlaying || EditorApplication.isPaused ||
                EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
            {
                return;
            }

            EditorApplication.update -= OnUpdate;

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // need to get scene via search because the path to the scene
                // file contains the package version so it'll change over time
                string[] guids = AssetDatabase.FindAssets("t:scene " + sceneToOpen, null);
                if (guids.Length == 0)
                {
                    Debug.LogWarning("Couldn't find scene file");
                }
                else
                {
                    string scenePath = AssetDatabase.GUIDToAssetPath(guids[0]);
                    EditorSceneManager.OpenScene(scenePath);
                    EditorApplication.isPlaying = isPlaying;
                }
            }
            sceneToOpen = null;
        }
    }
}