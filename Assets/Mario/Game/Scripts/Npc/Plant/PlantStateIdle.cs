using Mario.Game.Interactable;
using Mario.Game.Player;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateIdle : PlantState
    {
        #region Constructor
        public PlantStateIdle(Plant plant) : base(plant)
        {
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit();
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit();
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) { }//=> Plant.StateMachine.TransitionTo(Plant.StateMachine.StateDead);
        #endregion
    }
}
