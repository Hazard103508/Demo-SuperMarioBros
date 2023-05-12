using UnityEngine;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public abstract class Lerper : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        private float _current, _target;

        public UnityEvent onLerpCompleted;

        public float Speed { get; set; }
        protected bool IsRunning { get; set; }
        protected float CurrentCurve => GetCurrentCurve(_curve, _current);

        private void Awake() => Init();
        private void Update()
        {
            _current = Mathf.MoveTowards(_current, _target, Speed * Time.deltaTime);

            if (IsRunning)
                UpdateTarget();

            IsRunning = _current != _target;
        }

        protected abstract void UpdateTarget();
        protected virtual float GetCurrentCurve(AnimationCurve curve, float current) => curve.Evaluate(current);
        public abstract void Init();
        public void RunForward() => _target = 1;
        public void RunBackwards() => _target = 0;
    }
}