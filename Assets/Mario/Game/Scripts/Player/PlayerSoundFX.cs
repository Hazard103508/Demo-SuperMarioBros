using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioSource JumpSmallFX;
        [SerializeField] private AudioSource JumpBigFX;
        [SerializeField] private AudioSource NerfFX;

        public void PlayJumpSmall() => JumpSmallFX.Play();
        public void PlayJumpBig() => JumpBigFX.Play();
        public void PlayNerf() => NerfFX.Play();
    }
}