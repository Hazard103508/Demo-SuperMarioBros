using UnityEngine;

namespace Mario.Game.Commons
{
    public class DisableOnAudioCompleted : MonoBehaviour
    {
        private AudioSource _audioSource;

        #region Unity Methods
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (!_audioSource.isPlaying)
                gameObject.SetActive(false);
        }
        #endregion
    }
}