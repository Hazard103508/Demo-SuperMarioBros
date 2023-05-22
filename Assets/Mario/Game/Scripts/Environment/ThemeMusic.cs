using Mario.Application.Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mario.Game.Environment
{
    public class ThemeMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private bool isHurry;

        private void Awake()
        {
            _audioSource.clip = AllServices.GameDataService.CurrentMapProfile.Sounds.MainTheme;
            AllServices.LifeService.OnLivesRemoved.AddListener(OnLivesRemoved);
        }
        private void OnDestroy() => AllServices.LifeService.OnLivesRemoved.RemoveListener(OnLivesRemoved);
        private void Start()
        {
            _audioSource.Play();
        }
        private void Update()
        {
            if (!isHurry && AllServices.TimeService.Time <= 100)
            {
                isHurry = true;
                _audioSource.clip = AllServices.GameDataService.CurrentMapProfile.Sounds.HurryTheme;
                _audioSource.Play();
                return;
            }
        }
        private void OnLivesRemoved()
        {
            _audioSource.Stop();
            Destroy(gameObject);
        }
    }
}