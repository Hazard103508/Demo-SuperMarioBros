using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateRising : MushroomState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 1f;
        private float _initPosition;
        private float _targetPosition;
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
            _isFrozen = false;
            _timer = 0;
            Mushroom.Movable.enabled = false;
            Mushroom.gameObject.layer = LayerMask.NameToLayer("Item");

            _initPosition = Mushroom.transform.transform.position.y;
            _targetPosition = _initPosition + 1;
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

            float y = Mathf.Lerp(_initPosition, _targetPosition, t);
            Mushroom.transform.localPosition = new Vector3(Mushroom.transform.localPosition.x, y, Mushroom.transform.localPosition.z);

            if (_timer >= _maxTime)
                Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        }
        #endregion
    }
}