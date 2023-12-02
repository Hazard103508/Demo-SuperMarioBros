using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateRising : MushroomState
    {
        #region Objects
        private IGameplayService _gameplayService;

        private float _timer = 0;
        private float _maxTime = 0.8f;
        private Vector3 _initPosition;
        private Vector3 _targetPosition;
        private bool _isFrozen;
        #endregion

        #region Constructor
        public MushroomStateRising(Mushroom mushroom) : base(mushroom)
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region Private Methods
        private void GameplayService_GameUnfreezed() => _isFrozen = false;
        private void GameplayService_GameFreezed() => _isFrozen = true;
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

            _gameplayService.GameFreezed += GameplayService_GameFreezed;
            _gameplayService.GameUnfreezed += GameplayService_GameUnfreezed;
        }
        public override void Exit()
        {
            base.Exit();
            _gameplayService.GameFreezed -= GameplayService_GameFreezed;
            _gameplayService.GameUnfreezed -= GameplayService_GameUnfreezed;
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