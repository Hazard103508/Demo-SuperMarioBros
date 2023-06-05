using TMPro.EditorUtilities;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IMusicService : IGameService
    {
        AudioClip Clip { get; set; }
        float Time { get; set; }

        void Play();
        void Pause();
        void Stop();
    }
}