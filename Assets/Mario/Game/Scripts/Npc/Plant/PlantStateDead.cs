using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Plant
{
    public class PlantStateDead : PlantState
    {
        #region Objects
        private readonly IScoreService _scoreService;
        private readonly ISoundService _soundService;
        #endregion

        #region Constructor
        public PlantStateDead(Plant plant) : base(plant)
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _soundService.Play(Plant.Profile.KickSoundFXPoolReference, Plant.transform.position);
            _scoreService.Add(Plant.Profile.PointsKill);
            _scoreService.ShowPoints(Plant.Profile.PointsKill, Plant.transform.position + Vector3.up * 2f, 0.8f, 3f);
            Plant.gameObject.SetActive(false);
        }
        #endregion
    }
}
