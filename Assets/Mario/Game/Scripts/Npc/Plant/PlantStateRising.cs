using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateRising : PlantState
    {
        #region Objects
        private float _timer = 0;
        private float _maxTime = 1f;
        private float _initPosition;
        private float _targetPosition;
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

            _initPosition = Plant.transform.transform.position.y;
            _targetPosition = _initPosition + 2;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            var t = Mathf.InverseLerp(0, _maxTime, _timer);

            float y = Mathf.Lerp(_initPosition, _targetPosition, t);
            Plant.transform.localPosition = new Vector3(Plant.transform.localPosition.x, y, Plant.transform.localPosition.z);

            if (_timer >= _maxTime)
                Plant.StateMachine.TransitionTo(Plant.StateMachine.StateIdle);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => player.Hit(Plant);
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit(Plant);
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit(Plant);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => Kill(fireball.transform.position);
        #endregion
    }
}