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
            AllServices.TimeService.OnHurryUpTimeStart.AddListener(OnHurryUpTimeStart);
            LoadThemeSong();
        }
        private void Start()
        {
            AllServices.MusicService.Play();
        }
        private void OnDestroy()
        {
            AllServices.MusicService.Stop();
            AllServices.TimeService.OnHurryUpTimeStart.RemoveListener(OnHurryUpTimeStart);
        }
        #endregion

        #region Private Methods
        private void OnHurryUpTimeStart()
        {
            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.HurryFX;
            AllServices.MusicService.Play();

            StartCoroutine(PlayHurryTheme());
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
                    if (AllServices.TimeService.IsHurry)
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
        private IEnumerator PlayHurryTheme()
        {
            yield return new WaitForSeconds(3.5f);

            while (!AllServices.TimeService.Enabled)
                yield return null;

            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.Clip;
            AllServices.MusicService.Time = AllServices.GameDataService.CurrentMapProfile.Music.HurryThemeFirstTime.StartTime;
            AllServices.MusicService.Play();
        }
        #endregion
    }
}