using UnityEngine;

namespace Mario.Game.Interactable
{
    public class DisableOnAudioCompleted : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        #region Unity Methods
        private void Update()
        {
            if (!_audioSource.isPlaying)
                gameObject.SetActive(false);
        }
        #endregion
    }
}