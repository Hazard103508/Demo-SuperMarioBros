using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public class LocalPositionSlerper : Lerper
    {
        [SerializeField] protected Vector3 _centerPosition;
        [SerializeField] private Vector3 _goalPosition;
        private Vector3 _initPosition;

        public override void Init() => _initPosition = transform.localPosition;

        protected override void UpdateTarget()
        {
            var startRelativeCenter = _initPosition - _centerPosition;
            var endRelativeCenter = _goalPosition - _centerPosition;
            transform.localPosition = Vector3.Slerp(startRelativeCenter, endRelativeCenter, CurrentCurve) + _centerPosition;
        }
    }
}