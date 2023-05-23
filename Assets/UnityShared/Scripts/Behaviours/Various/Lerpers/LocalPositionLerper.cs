using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public class LocalPositionLerper : Lerper
    {
        public Vector3 GoalPosition;
        private Vector3 _initPosition;

        public override void Init() => _initPosition = transform.localPosition;
        protected override void UpdateTarget()
        {
            transform.localPosition = Vector3.Lerp(_initPosition, GoalPosition, CurrentCurve);

            if (transform.localPosition == GoalPosition)
            {
                onLerpCompleted.Invoke();
                IsRunning = false;
            }
        }
    }
}