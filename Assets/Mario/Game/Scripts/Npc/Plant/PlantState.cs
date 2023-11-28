using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;

namespace Mario.Game.Npc.Plant
{
    public abstract class PlantState :
        IState,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByFireBall
    {
        #region State Machine
        public virtual void Enter()
        {
        }
        public virtual void Exit()
        {
        }
        public virtual void Update()
        {
        }
        #endregion

        #region Properties
        protected Plant Plant { get; private set; }
        #endregion

        #region Constructor
        public PlantState(Plant plant)
        {
            Plant = plant;
        }
        #endregion

        #region On Player Hit
        public virtual void OnHittedByPlayerFromTop(PlayerController player)
        {
        }
        public virtual void OnHittedByPlayerFromLeft(PlayerController player)
        {
        }
        public virtual void OnHittedByPlayerFromRight(PlayerController player)
        {
        }
        #endregion

        #region On Fireball Hit
        public virtual void OnHittedByFireBall(Fireball fireball)
        {
        }
        #endregion
    }
}
