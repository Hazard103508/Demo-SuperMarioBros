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
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.AddListener(OnGoalReached);

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.None)
                Destroy(this);
        }
        private void OnDestroy()
        {
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.RemoveListener(OnGoalReached);
        }
        #endregion

        #region Private Methods
        private void ValidScoreCount()
        {
            if (AllServices.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - AllServices.TimeService.Time;
                AllServices.ScoreService.Add(_timedif * _pointsPerSecond);

                _timeScoreFX.Play();
                // se repduce cada al cambiar el valor del tiempo, 
                // esta horrible, pero no encontre un audio clip con el sonido correcto
            }
            _previousTime = AllServices.TimeService.Time;
        }
        #endregion

        #region Service Events	
        public void OnGoalReached() => AllServices.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged() => ValidScoreCount();
        #endregion
    }
}