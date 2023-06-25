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

        private void Awake() => AllServices.PlayerService.OnLivesAdded.AddListener(OnLivesAdded);
        private void OnDestroy() => AllServices.PlayerService.OnLivesAdded.RemoveListener(OnLivesAdded);

        public void PlayJumpSmall() => _jumpSmallFX.Play();
        public void PlayJumpBig() => _jumpBigFX.Play();
        public void PlayNerf() => _nerfFX.Play();
        public void PlayBuff() => _buffFX.Play();
        public void Play1Up() => _1UpFX.Play();


        private void OnLivesAdded() => _1UpFX.Play();
    }
}