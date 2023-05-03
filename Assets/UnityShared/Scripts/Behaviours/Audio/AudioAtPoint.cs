using UnityEngine;

namespace UnityShared.Behaviours.Audio
{

    public class AudioAtPoint : MonoBehaviour
    {
        public AudioClip[] audioClips;
        public Transform audioPoint;
        [Range(0, 1)] public float audioVolume = 0.5f;

        public void PlayRandom()
        {
            var index = Random.Range(0, audioClips.Length);
            AudioSource.PlayClipAtPoint(audioClips[index], audioPoint.position, audioVolume);
        }
    }
}