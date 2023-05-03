using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public class LocalPositionLerper : Lerper
    {
        [SerializeField] private Vector3 _goalPosition;
        private Vector3 _initPosition;

        public override void Init() => _initPosition = transform.position;
        protected override void UpdateTarget() => transform.localPosition = Vector3.Lerp(_initPosition, _goalPosition, CurrentCurve);
    }
}