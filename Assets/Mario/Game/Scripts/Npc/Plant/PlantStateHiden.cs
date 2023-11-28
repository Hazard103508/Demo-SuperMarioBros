using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateHiden : PlantState
    {
        #region Objects
        private float _timer;
        #endregion

        #region Constructor
        public PlantStateHiden(Plant plant) : base(plant)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _timer = 0;
        }
        public override void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= Plant.Profile.TimeHiden)
                Plant.StateMachine.TransitionTo(Plant.StateMachine.StateRising);
        }
        #endregion
    }
}
