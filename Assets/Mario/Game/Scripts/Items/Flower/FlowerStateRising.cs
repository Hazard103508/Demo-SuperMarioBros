using Mario.Game.Player;
using System.Net.Sockets;
using UnityEngine;

namespace Mario.Game.Items.Flower
{
    public class FlowerStateRising : FlowerState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 1f;
        private float _collectTime = 0.4f;
        private float _initPosition;
        private float _targetPosition;
        private bool _isFrozen;
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
            Flower.gameObject.layer = LayerMask.NameToLayer("Item");

            _initPosition = Flower.transform.transform.position.y;
            _targetPosition = _initPosition + 1;
        }
        public override void Update()
        {
            if (_isFrozen)
                return;

            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);

            float y = Mathf.Lerp(_initPosition, _targetPosition, t);
            Flower.transform.localPosition = new Vector3(Flower.transform.localPosition.x, y, Flower.transform.localPosition.z);

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