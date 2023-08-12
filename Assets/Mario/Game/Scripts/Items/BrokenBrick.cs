using UnityEngine;

namespace Mario.Game.Items
{
    public class BrokenBrick : MonoBehaviour
    {
        #region Objects
        [SerializeField] private AudioSource _hitFXAudioSource;
        [SerializeField] private AudioSource _brickFXAudioSource;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            _hitFXAudioSource.Play();
            _brickFXAudioSource.Play();
        }
        #endregion

        #region Public Methods
        public void OnAnimationCompleted() => gameObject.SetActive(false);
        #endregion
    }
}