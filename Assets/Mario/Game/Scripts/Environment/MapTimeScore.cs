using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapTimeScore : MonoBehaviour
    {
        #region Objects
        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private int _pointsPerSecond = 50;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            Services.GameDataService.OnGoalReached.AddListener(OnGoalReached);

            if (Services.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.None)
                Destroy(this);
        }
        private void OnDestroy()
        {
            Services.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            Services.GameDataService.OnGoalReached.RemoveListener(OnGoalReached);
        }
        #endregion

        #region Private Methods
        private void ValidScoreCount()
        {
            if (Services.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - Services.TimeService.Time;
                Services.ScoreService.Add(_timedif * _pointsPerSecond);

                _timeScoreFX.Play();
                // se repduce cada al cambiar el valor del tiempo, 
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