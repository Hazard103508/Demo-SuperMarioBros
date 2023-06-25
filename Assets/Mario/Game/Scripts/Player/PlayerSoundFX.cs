using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioSource JumpSmallFX;
        [SerializeField] private AudioSource JumpBigFX;
        [SerializeField] private AudioSource NerfFX;
        [SerializeField] private AudioSource BuffFX;

        public void PlayJumpSmall() => JumpSmallFX.Play();
        public void PlayJumpBig() => JumpBigFX.Play();
        public void PlayNerf() => NerfFX.Play();
        public void PlayBuff() => BuffFX.Play();
    }
}