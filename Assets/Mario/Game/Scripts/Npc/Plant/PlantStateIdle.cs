using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateIdle : PlantState
    {
        #region Objects
        private float _timer;
        #endregion

        #region Constructor
        public PlantStateIdle(Plant plant) : base(plant)
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

            if (_timer >= Plant.Profile.TimeVisible)
                Plant.StateMachine.TransitionTo(Plant.StateMachine.StateHiding);
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
