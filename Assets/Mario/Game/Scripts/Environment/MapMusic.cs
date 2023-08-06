using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Map;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class MapMusic : MonoBehaviour
    {
        #region Unity Methods
        private void Awake()
        {
            Services.TimeService.OnHurryUpTimeStart.AddListener(OnHurryUpTimeStart);
        }
        private void Start()
        {
            LoadThemeSong();
            Services.MusicService.Play();
        }
        private void OnDestroy()
        {
            Services.MusicService.Stop();
            Services.TimeService.OnHurryUpTimeStart.RemoveListener(OnHurryUpTimeStart);
        }
        #endregion

        #region Private Methods
        private void OnHurryUpTimeStart()
        {
            Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryFX;
            Services.MusicService.Play();

            StartCoroutine(PlayHurryTheme());
        }
        private void LoadThemeSong()
        {
            switch (Services.GameDataService.CurrentMapProfile.Time.Type)
            {
                case MapTimeType.None:
                    Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                    Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    break;
                case MapTimeType.Beginning:
                case MapTimeType.Continuated:
                    if (Services.TimeService.IsHurry)
                    {
                        Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryTheme.Clip;
                        Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.HurryTheme.StartTime;
                    }
                    else
                    {
                        Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.MainTheme.Clip;
                        Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.MainTheme.StartTime;
                    }
                    break;
            }
        }
        private IEnumerator PlayHurryTheme()
        {
            yield return new WaitForSeconds(3.5f);

            while (!Services.TimeService.Enabled)
                yield return null;

            Services.MusicService.Clip = Services.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.Clip;
            Services.MusicService.Time = Services.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.StartTime;
            Services.MusicService.Play();
        }
        #endregion
    }
}