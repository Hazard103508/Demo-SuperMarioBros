using UnityEngine;

namespace Mario.Game.Items.Star
{
    public class StarStateRising : StarState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 0.8f;
        private Vector3 _initPosition;
        private Vector3 _targetPosition;
        private bool _isFrozen;
        #endregion

        #region Constructor
        public StarStateRising(Star star) : base(star)
        {
        }
        #endregion

        #region Public Methods
        public override void OnGameFrozen() => _isFrozen = true;
        public override void OnGameUnfrozen() => _isFrozen = false;
        #endregion

        #region IState Methods
        public override void Enter()
        {
            base.Enter();
            _timer = 0;
            Star.Movable.enabled = false;
            Star.gameObject.layer = LayerMask.NameToLayer("Item");
            _initPosition = Star.transform.transform.position;
            _targetPosition = _initPosition + Vector3.up;
        }
        public override void Update()
        {
            if (_isFrozen)
                return;

            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Star.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Star.StateMachine.TransitionTo(Star.StateMachine.StateJumping);
        }
        #endregion
    }
}