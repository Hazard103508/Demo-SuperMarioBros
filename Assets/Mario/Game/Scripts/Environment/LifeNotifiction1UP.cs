using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class LifeNotifiction1UP : MonoBehaviour
    {
        [SerializeField] private AudioSource _1UpFX;

        private void Awake() => AllServices.PlayerService.OnLivesAdded.AddListener(OnLivesChanged);
        private void OnDestroy() => AllServices.PlayerService.OnLivesAdded.RemoveListener(OnLivesChanged);

        private void OnLivesChanged() => _1UpFX.Play();
    }
}