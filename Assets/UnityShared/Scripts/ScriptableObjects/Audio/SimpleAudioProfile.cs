using System;
using System.Collections;
using UnityEngine;
using UnityShared.Helpers;
using Random = UnityEngine.Random;

namespace UnityShared.ScriptableObjects.Audio
{
    [CreateAssetMenu(fileName = "SimpleAudio", menuName = "ScriptableObjects/Audio/SimpleAudio", order = 0)]
    public class SimpleAudioProfile : AudioProfile
    {
        public AudioClip[] Clips;
        public RangedFloat Volume;
        public RangedFloat Pitch;
        [Header("Runtime Only:")]
        public RangedFloat Delay;

        public override void Play(AudioSource audioSource)
        {
            if (Clips.Length == 0)
                return;

#if UNITY_EDITOR
            if (Application.isPlaying)
                CoroutineHelper.Instance.StartCoroutine(CO_Sequence(audioSource));
            else
                SFX(audioSource);
#else
            CoroutineHelper.Instance.StartCoroutine(CO_Sequence(audioSource));
#endif
        }

        private void SFX(AudioSource audioSource)
        {
            audioSource.clip = Clips[Random.Range(0, Clips.Length)];
            audioSource.volume = Random.Range(Volume.Min, Volume.Max);
            audioSource.pitch = Random.Range(Pitch.Min, Pitch.Max);
            audioSource.Play();
        }

        private IEnumerator CO_Sequence(AudioSource audioSource)
        {
            var timeLapse = Random.Range(Delay.Min, Delay.Max);
            yield return new WaitForSeconds(timeLapse);
            SFX(audioSource);
        }
    }

    [Serializable]
    public class RangedFloat
    {
        [Range(0f, 2f)]
        public float Min;
        [Range(0f, 2f)]
        public float Max;
    }
}