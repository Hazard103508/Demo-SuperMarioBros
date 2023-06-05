using Mario.Application.Interfaces;
using UnityEngine;

namespace Mario.Application.Services
{
    public class MusicService : MonoBehaviour, IMusicService
    {
        [SerializeField] private AudioSource _audioSource;
        public AudioClip Clip { get => _audioSource.clip; set => _audioSource.clip = value; }
        public float Time { get => _audioSource.time; set => _audioSource.time = value; }

        public void LoadService()
        {
        }

        public void Play() => _audioSource.Play();
        public void Pause() => _audioSource.Pause();
        public void Stop() => _audioSource.Stop();
    }
}