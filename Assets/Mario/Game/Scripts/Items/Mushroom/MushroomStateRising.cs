using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateRising : MushroomState
    {
        #region Objects
        float _timer = 0;
        float _maxTime = 0.8f;
        float _collectTime = 0.4f;
        Vector3 _initPosition;
        Vector3 _targetPosition;
        #endregion

        #region Constructor
        public MushroomStateRising(Mushroom mushroom) : base(mushroom)
        {
        }
        #endregion

        #region Private Methods
        private void ChangeStateToCollect()
        {
            if (_timer >= _collectTime)
                Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Mushroom.Movable.enabled = false;
            Mushroom.gameObject.layer = LayerMask.NameToLayer("Item");
            _initPosition = Mushroom.transform.transform.position;
            _targetPosition = _initPosition + Vector3.up;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Mushroom.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateWalk);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController_OLD player) => ChangeStateToCollect();
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player) => ChangeStateToCollect();
        public override void OnHittedByPlayerFromLeft(PlayerController_OLD player) => ChangeStateToCollect();
        public override void OnHittedByPlayerFromRight(PlayerController_OLD player) => ChangeStateToCollect();
        #endregion
    }
}