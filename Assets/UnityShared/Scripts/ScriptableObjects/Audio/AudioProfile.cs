using UnityEngine;

namespace UnityShared.ScriptableObjects.Audio
{
    public abstract class AudioProfile : ScriptableObject
    {
        public abstract void Play(AudioSource audioSource);
    }
}