using UnityEngine;

namespace Mario.Game.Items
{
    public class SingleSoundFX : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        private void Update()
        {
            if (!_audioSource.isPlaying)
                Destroy(gameObject);
        }
    }
}