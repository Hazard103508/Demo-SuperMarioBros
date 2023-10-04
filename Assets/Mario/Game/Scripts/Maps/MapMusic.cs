using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Maps
{
    public class MapMusic : MonoBehaviour
    {
        #region Objects
        private IThemeMusicService _themeMusicService;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _themeMusicService = ServiceLocator.Current.Get<IThemeMusicService>();
            Services.TimeService.HurryUpTimeStarted += OnHurryUpTimeStart;
        }
        private void Start()
        {
            LoadThemeSong();
            _themeMusicService.Play();
        }
        private void OnDestroy()
        {
            _themeMusicService.Stop();
            Services.TimeService.HurryUpTimeStarted -= OnHurryUpTimeStart;
        }
        #endregion

        #region Private Methods
        private void OnHurryUpTimeStart()
        {
            _themeMusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryFX;
            _themeMusicService.Play();

            StartCoroutine(PlayHurryTheme());
        }
        private void LoadThemeSong()
        {
            switch (Services.GameDataService.CurrentMapProfile.Time.Type)
            {
                case MapTimeType.None:
                    _themeMusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                    _themeMusicService.Time = Services.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    break;
                case MapTimeType.Beginning:
                case MapTimeType.Continuated:
                    if (Services.TimeService.IsHurry)
                    {
                        _themeMusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryTheme.Clip;
                        _themeMusicService.Time = Services.GameDataService.CurrentMapProfile.Music.HurryTheme.StartTime;
                    }
                    else
                    {
                        _themeMusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                        _themeMusicService.Time = Services.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    }
                    break;
            }
        }
        private IEnumerator PlayHurryTheme()
        {
            yield return new WaitForSeconds(3.5f);

            while (!Services.TimeService.Enabled)
                yield return null;

            _themeMusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.Clip;
            _themeMusicService.Time = Services.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.StartTime;
            _themeMusicService.Play();
        }
        #endregion
    }
}