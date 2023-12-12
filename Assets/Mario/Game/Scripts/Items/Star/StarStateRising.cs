using UnityEngine;

namespace Mario.Game.Items.Star
{
    public class StarStateRising : StarState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 1f;
        private float _initPosition;
        private float _targetPosition;
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
            _isFrozen = false;
            Star.Movable.enabled = false;
            Star.gameObject.layer = LayerMask.NameToLayer("Item");
            
            _initPosition = Star.transform.transform.position.y;
            _targetPosition = _initPosition + 1;
        }
        public override void Update()
        {
            if (_isFrozen)
                return;

            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);

            float y = Mathf.Lerp(_initPosition, _targetPosition, t);
            Star.transform.localPosition = new Vector3(Star.transform.localPosition.x, y, Star.transform.localPosition.z);

            if (_timer >= _maxTime)
                Star.StateMachine.TransitionTo(Star.StateMachine.StateJumping);
        }
        #endregion
    }
}