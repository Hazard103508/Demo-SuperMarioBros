using Mario.Application.Services;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class ThemeMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private bool isHurry;

        private void Awake()
        {
            _audioSource.clip = AllServices.GameDataService.CurrentMapProfile.MapInit.MainTheme;
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
            _audioSource.Play();
        }
        private void Update()
        {
            PlayHurryTheme();
        }
        private void OnLivesRemoved() => _audioSource.Stop();
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
                _audioSource.clip = AllServices.GameDataService.CurrentMapProfile.Time.HurryTheme;
                _audioSource.Play();
                return;
            }
        }
        private IEnumerator PlayVictoryTheme()
        {
            _audioSource.Stop();
            yield return new WaitForSeconds(1);
            _audioSource.clip = AllServices.GameDataService.CurrentMapProfile.EndPoint.VictoryTheme;
            _audioSource.Play();
        }
    }
}