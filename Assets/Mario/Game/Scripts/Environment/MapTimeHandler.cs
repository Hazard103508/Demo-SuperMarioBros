using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapTimeHandler : MonoBehaviour
    {
        [SerializeField] private AudioSource _timeScoreFX;
        private int _previousTime;
        private bool _isHurry;
        private int _hurryTime = 100;


        private void Awake()
        {
            AllServices.TimeService.OnTimeChanged.AddListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.AddListener(OnGoalReached);

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.None)
                Destroy(this);

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == MapTimeType.Continuated && AllServices.TimeService.Time <= _hurryTime)
                _isHurry = true;

            LoadThemeSong();
        }
        private void Start()
        {
            AllServices.MusicService.Play();
        }
        private void OnDestroy()
        {
            AllServices.MusicService.Stop();
            AllServices.TimeService.OnTimeChanged.RemoveListener(OnTimeChanged);
            AllServices.GameDataService.OnGoalReached.RemoveListener(OnGoalReached);
        }

        public void OnGoalReached() => AllServices.TimeService.TimeSpeed = 150f;
        public void OnTimeChanged()
        {
            ValidScoreCount();
            ValidTimeOut();

            _previousTime = AllServices.TimeService.Time;
        }

        private void LoadThemeSong()
        {
            switch (AllServices.GameDataService.CurrentMapProfile.Time.Type)
            {
                case MapTimeType.None:
                    AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                    AllServices.MusicService.Time = AllServices.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    break;
                case MapTimeType.Beginning:
                case MapTimeType.Continuated:
                    if (_isHurry)
                    {
                        AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.HurryTheme.Clip;
                        AllServices.MusicService.Time = AllServices.GameDataService.CurrentMapProfile.Music.HurryTheme.StartTime;
                    }
                    else
                    {
                        AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                        AllServices.MusicService.Time = AllServices.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    }
                    break;
            }
        }
        private void ValidScoreCount()
        {
            if (AllServices.GameDataService.IsGoalReached)
            {
                int _timedif = _previousTime - AllServices.TimeService.Time;
                AllServices.ScoreService.Add(_timedif * AllServices.GameDataService.CurrentMapProfile.EndPoint.RemainingTimePoints);

                _timeScoreFX.Play();
            }
        }
        private void ValidTimeOut()
        {
            if (!_isHurry && AllServices.TimeService.Time <= _hurryTime && !AllServices.GameDataService.IsGoalReached)
            {
                _isHurry = true;
                AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.HurryFX;
                AllServices.MusicService.Play();

                StartCoroutine(PlayHurryTheme());
                return;
            }
        }
        private IEnumerator PlayHurryTheme()
        {
            yield return new WaitForSeconds(3.5f);
            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.Clip;
            AllServices.MusicService.Time = AllServices.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.StartTime;
            AllServices.MusicService.Play();
        }
    }
}