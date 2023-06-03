using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class ThemeMusic : MonoBehaviour
    {
        private bool isHurry;

        private void Awake()
        {
            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.MapInit.MainTheme;
            AllServices.PlayerService.OnLivesRemoved.AddListener(OnLivesRemoved);
            AllServices.GameDataService.OnFlagReached.AddListener(OnMapCompleted);
        }
        private void OnDestroy()
        {
            AllServices.PlayerService.OnLivesRemoved.RemoveListener(OnLivesRemoved);
            AllServices.GameDataService.OnFlagReached.RemoveListener(OnMapCompleted);
        }
        private void Start()
        {
            AllServices.MusicService.Play();
        }
        private void Update()
        {
            PlayHurryTheme();
        }
        private void OnLivesRemoved() => AllServices.MusicService.Stop();
        private void OnMapCompleted() => StartCoroutine(PlayVictoryTheme());

        private void PlayHurryTheme()
        {
            if (AllServices.GameDataService.IsMapCompleted)
                return;

            if (AllServices.GameDataService.CurrentMapProfile.Time.Type == Game.ScriptableObjects.Map.MapTimeType.None)
                return;

            if (!isHurry && AllServices.TimeService.Time <= 100)
            {
                isHurry = true;
                AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.Time.HurryTheme;
                AllServices.MusicService.Play();
                return;
            }
        }
        private IEnumerator PlayVictoryTheme()
        {
            AllServices.MusicService.Stop();
            yield return new WaitForSeconds(1);
            AllServices.MusicService.Clip = AllServices.GameDataService.CurrentMapProfile.EndPoint.VictoryTheme;
            AllServices.MusicService.Play();
        }
    }
}