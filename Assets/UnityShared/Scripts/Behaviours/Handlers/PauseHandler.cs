using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Handlers
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPaused => Time.timeScale == 0;
        public UnityEvent onPaused;
        public UnityEvent onResumed;

        public void Pause()
        {
            Time.timeScale = 0;
            onPaused.Invoke();
        }

        public void Resume()
        {
            Time.timeScale = 1;
            onResumed.Invoke();
        }
    }
}