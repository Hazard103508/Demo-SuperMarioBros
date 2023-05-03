using UnityEngine;

namespace UnityShared.Behaviours.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
        }
    }
}