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
        private ITimeService _timeService;
        private ILevelService _levelService;

        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private int _pointsPerSecond = 50;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _timeService = ServiceLocator.Current.Get<ITimeService>();
            _levelService = ServiceLocator.Current.Get<ILevelService>();

            _timeService.TimeChangeded += OnTimeChanged;
            _levelService.GoalReached += OnGoalReached;

            if (_levelService.MapProfile.StartTime <= 0)
                Destroy(this);
        }
        private void OnDestroy()
        {
            _timeService.TimeChangeded -= OnTimeChanged;
            _levelService.GoalReached -= OnGoalReached;
        }
        #endregion

        #region Private Methods
        private void ValidScoreCount()
        {
            if (_levelService.IsGoalReached)
            {
                int _timedif = _previousTime - _timeService.Time;
                _scoreService.Add(_timedif * _pointsPerSecond);

                _timeScoreFX.Play();
                // se repduce al cambiar el valor del tiempo, 
                // esta horrible, pero no encontre un audio clip con el sonido correcto
            }
            _previousTime = _timeService.Time;
        }   
        #endregion

        #region Service Events	
        public void OnGoalReached() => _timeService.TimeSpeed = 150f;
        public void OnTimeChanged() => ValidScoreCount();
        #endregion
    }
}