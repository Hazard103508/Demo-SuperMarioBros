using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items.Flower
{
    public class FlowerStateRising : FlowerState
    {
        #region Objects
        float _timer = 0;
        float _maxTime = 0.8f;
        float _collectTime = 0.4f;
        Vector3 _initPosition;
        Vector3 _targetPosition;
        #endregion

        #region Constructor
        public FlowerStateRising(Flower flower) : base(flower)
        {
        }
        #endregion

        #region Protected Methods
        protected override void CollectFlower(PlayerController player)
        {
            if (_timer >= _collectTime)
                base.CollectFlower(player);
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
            Flower.gameObject.layer = LayerMask.NameToLayer("Item");
            _initPosition = Flower.transform.transform.position;
            _targetPosition = _initPosition + Vector3.up;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Flower.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Flower.StateMachine.TransitionTo(Flower.StateMachine.StateIdle);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => CollectFlower(player);
        public override void OnHittedByPlayerFromRight(PlayerController player) => CollectFlower(player);
        #endregion
    }
}