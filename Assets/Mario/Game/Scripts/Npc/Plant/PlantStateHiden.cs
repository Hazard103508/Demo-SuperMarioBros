using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateHiden : PlantState
    {
        #region Objects
        private IPlayerService _playerService;
        private float _timer;
        #endregion

        #region Constructor
        public PlantStateHiden(Plant plant) : base(plant)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
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
            {
                if (_playerService.IsPlayerNearX(Plant.transform.position.x, 2f))
                {
                    _timer = Plant.Profile.TimeHiden;
                    return;
                }

                Plant.StateMachine.TransitionTo(Plant.StateMachine.StateRising);
            }
        }
        #endregion
    }
}
