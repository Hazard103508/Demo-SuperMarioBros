using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public class LocalRotationLerper : Lerper
    {
        [SerializeField] private Vector3 _goalRotation;
        private Quaternion _initRotation;

        public override void Init() => _initRotation = transform.rotation;
        protected override void UpdateTarget() => transform.localRotation = Quaternion.Lerp(_initRotation, Quaternion.Euler(_goalRotation), CurrentCurve);
    }
}