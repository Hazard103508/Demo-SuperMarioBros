using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Interactable;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public abstract class PlantState :
        IEnemy,
        IState,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByFireBall
    {
        #region Object
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        #endregion

        #region Properties
        protected Plant Plant { get; private set; }
        #endregion

        #region Constructor
        public PlantState(Plant plant)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();

            Plant = plant;
        }
        #endregion

        #region Public Methods
        public void Kill(Vector3 hitPosition)
        {
            _soundService.Play(Plant.Profile.KickSoundFXPoolReference, Plant.transform.position);
            _scoreService.Add(Plant.Profile.PointsKill);
            _scoreService.ShowPoints(Plant.Profile.PointsKill, Plant.transform.position + Vector3.up * 2f, 0.8f, 3f);
            Plant.gameObject.SetActive(false);
        }
        #endregion

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
