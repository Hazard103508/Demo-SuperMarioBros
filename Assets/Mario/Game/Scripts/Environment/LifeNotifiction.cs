using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class LifeNotifiction : MonoBehaviour
    {
        [SerializeField] private AudioSource _1UpFX;

        private void OnEnable() => AllServices.LifeService.OnLivesChanged.AddListener(OnLivesChanged);
        private void OnDisable() => AllServices.LifeService.OnLivesChanged.RemoveListener(OnLivesChanged);

        private void OnLivesChanged() => _1UpFX.Play();
    }
}