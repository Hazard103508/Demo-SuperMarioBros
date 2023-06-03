using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Interfaces
{
    public interface IMusicService : IGameService
    {
        AudioClip Clip { get; set; }

        void Play();
        void Pause();
        void Stop();
    }
}