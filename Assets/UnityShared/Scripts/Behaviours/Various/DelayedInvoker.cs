using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Various
{
    public class DelayedInvoker : MonoBehaviour
    {
        public float delayStartTimeSeconds;
        public float minTimeSeconds;
        public float maxTimeSeconds;

        public UnityEvent onActionStart;

        void Start()
        {
            Invoke("RunAction", delayStartTimeSeconds);
        }

        private void RunAction()
        {
            onActionStart.Invoke();
            float delay = Random.Range(minTimeSeconds, maxTimeSeconds);
            Invoke("RunAction", delay);
        }
    }
}