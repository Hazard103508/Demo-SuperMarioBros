using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class MapTimeScore : MonoBehaviour
    {
        #region Objects
        private IScoreService _scoreService;

        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private int _pointsPerSecond = 50;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();

            Services.TimeService.TimeChangeded += OnTimeChanged;
            Services.GameDataService.GoalReached += OnGoalReached;

            if (Services.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.None)
                Destroy(this);
        }
        private void OnDestroy()
        {
            Services.TimeService.TimeChangeded -= OnTimeChanged;
            Services.GameDataService.GoalReached -= OnGoalReached;
        }
        #endregion

        #region Private Methods
        private void ValidScoreCount()
        {
            if (Services.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - Services.TimeService.Time;
                _scoreService.Add(_timedif * _pointsPerSecond);

                _timeScoreFX.Play();
                // se repduce al cambiar el valor del tiempo, 
                // esta horrible, pero no encontre un audio clip con el sonido correcto
            }
            _previousTime = Services.TimeService.Time;
        }
        #endregion

        #region Service Events	
        public void OnGoalReached() => Services.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged() => ValidScoreCount();
        #endregion
    }
}