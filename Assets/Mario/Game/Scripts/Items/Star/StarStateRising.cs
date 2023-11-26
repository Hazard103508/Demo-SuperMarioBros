using UnityEngine;

namespace Mario.Game.Items.Star
{
    public class StarStateRising : StarState
    {
        #region Objects
        float _timer = 0;
        float _maxTime = 0.8f;
        Vector3 _initPosition;
        Vector3 _targetPosition;
        #endregion

        #region Constructor
        public StarStateRising(Star star) : base(star)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Star.Movable.enabled = false;
            Star.gameObject.layer = LayerMask.NameToLayer("Item");
            _initPosition = Star.transform.transform.position;
            _targetPosition = _initPosition + Vector3.up;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Star.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Star.StateMachine.TransitionTo(Star.StateMachine.StateJumping);
        }
        #endregion
    }
}