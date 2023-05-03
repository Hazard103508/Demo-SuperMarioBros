using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public class LocalScaleLerper : Lerper
    {
        [SerializeField] private Vector3 _goalScale;
        [SerializeField] private bool pingPong;
        private Vector3 _initScale;

        public override void Init() => _initScale = transform.localScale;
        protected override float GetCurrentCurve(AnimationCurve curve, float current) => pingPong ? curve.Evaluate(Mathf.PingPong(current, 0.5f) * 2f) : base.GetCurrentCurve(curve, current);
        protected override void UpdateTarget() => transform.localScale = Vector3.Lerp(_initScale, _goalScale, CurrentCurve);
    }
}