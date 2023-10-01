using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IThemeMusicService : IGameService
    {
        AudioClip Clip { get; set; }
        float Time { get; set; }

        void Play();
        void Stop();
    }
}