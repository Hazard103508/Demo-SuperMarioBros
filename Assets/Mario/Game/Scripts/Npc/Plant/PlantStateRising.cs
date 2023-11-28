using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateRising : PlantState
    {
        #region Objects
        float _timer = 0;
        float _maxTime = 1f;
        Vector3 _initPosition;
        Vector3 _targetPosition;
        #endregion

        #region Constructor
        public PlantStateRising(Plant plant) : base(plant)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;

            if(_initPosition == Vector3.zero)
                _initPosition = Plant.transform.transform.position;
            else
                Plant.transform.transform.position = _initPosition;

            _targetPosition = _initPosition + (2 * Vector3.up);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);
            Plant.transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);

            if (_timer >= _maxTime)
                Plant.StateMachine.TransitionTo(Plant.StateMachine.StateIdle);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit();
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball)
        {
            Plant.StateMachine.TransitionTo(Plant.StateMachine.StateDead);
        }
        #endregion
    }
}