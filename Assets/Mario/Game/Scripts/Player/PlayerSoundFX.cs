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

        public void PlayJumpSmall() => _jumpSmallFX.Play();
        public void PlayJumpBig() => _jumpBigFX.Play();
        public void PlayNerf() => _nerfFX.Play();
        public void PlayBuff() => _buffFX.Play();
    }
}