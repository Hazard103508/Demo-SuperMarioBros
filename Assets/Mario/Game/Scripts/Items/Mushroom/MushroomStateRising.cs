using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateRising : MushroomState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 0.8f;
        private Vector3 _initPosition;
        private Vector3 _targetPosition;
        private bool _isFrozen;
        #endregion

        #region Constructor
        public MushroomStateRising(Mushroom mushroom) : base(mushroom)
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
            Mushroom.Movable.enabled = false;
            Mushroom.gameObject.layer = LayerMask.NameToLayer("Item");
            _initPosition = Mushroom.transform.transform.position;
            _targetPosition = _initPosition + Vector3.up;
        }
        public override void Exit()
        {
            base.Exit();
        }
        public override void Update()
        {
            if (_isFrozen)
                return;

            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Mushroom.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        }
        #endregion
    }
}