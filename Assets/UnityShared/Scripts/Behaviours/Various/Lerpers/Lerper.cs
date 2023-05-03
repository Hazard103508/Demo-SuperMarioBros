using UnityEngine;

namespace UnityShared.Behaviours.Various.Lerpers
{
    public abstract class Lerper : MonoBehaviour
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _speed;

        private bool _isRunning;
        private float _current, _target;

        protected float CurrentCurve => GetCurrentCurve(_curve, _current);

        private void Awake() => Init();
        private void Update()
        {
            _current = Mathf.MoveTowards(_current, _target, _speed * Time.deltaTime);

            if (_isRunning)
                UpdateTarget();

            _isRunning = _current != _target;
        }

        protected abstract void UpdateTarget();
        protected virtual float GetCurrentCurve(AnimationCurve curve, float current) => curve.Evaluate(current);
        public abstract void Init();
        public void RunForward() => _target = 1;
        public void RunBackwards() => _target = 0;
    }
}