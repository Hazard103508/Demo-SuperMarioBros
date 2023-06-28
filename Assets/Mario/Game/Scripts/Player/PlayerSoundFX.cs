using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioSource _jumpSmallFX;
        [SerializeField] private AudioSource _jumpBigFX;
        [SerializeField] private AudioSource _nerfFX;
        [SerializeField] private AudioSource _buffFX;
        [SerializeField] private AudioSource _1UpFX;
        [SerializeField] private AudioSource _deadFX;

        private void Awake()
        {
            AllServices.PlayerService.OnLivesAdded.AddListener(OnLivesAdded);
            AllServices.PlayerService.OnLivesRemoved.AddListener(OnLivesRemoved);
        }
        private void OnDestroy()
        {
            AllServices.PlayerService.OnLivesAdded.RemoveListener(OnLivesAdded);
            AllServices.PlayerService.OnLivesRemoved.RemoveListener(OnLivesRemoved);
        }

        public void PlayJumpSmall() => _jumpSmallFX.Play();
        public void PlayJumpBig() => _jumpBigFX.Play();
        public void PlayNerf() => _nerfFX.Play();
        public void PlayBuff() => _buffFX.Play();
        public void Play1Up() => _1UpFX.Play();
        public void PlayDead() => _deadFX.Play();


        private void OnLivesAdded() => _1UpFX.Play();
        private void OnLivesRemoved() => PlayDead();
    }
}