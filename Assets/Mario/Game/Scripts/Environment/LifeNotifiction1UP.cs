using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class LifeNotifiction1UP : MonoBehaviour
    {
        [SerializeField] private AudioSource _1UpFX;

        private void OnEnable() => AllServices.LifeService.OnLivesAdded.AddListener(OnLivesChanged);
        private void OnDisable() => AllServices.LifeService.OnLivesAdded.RemoveListener(OnLivesChanged);

        private void OnLivesChanged() => _1UpFX.Play();
    }
}