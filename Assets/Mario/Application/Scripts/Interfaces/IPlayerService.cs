using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Player;
using System;
using UnityEngine;

namespace Mario.Application.Interfaces
{
    public interface IPlayerService : IGameService
    {
        PlayerProfile PlayerProfile { get; }
        int Lives { get; }

        event Action LivesAdded;
        event Action LivesRemoved;

        void EnablePlayerController(bool enable);
        void EnablePlayerMovable(bool enable);
        void EnablePlayerInput(bool enable);
        void SetPlayer(PlayerController playerController);
        void SetPlayerPosition(Vector3 position);
        void TranslatePlayerPosition(Vector3 position);
        void KillPlayer();
        void KillPlayerByTimeOut();
        void AddLife();
        void RemoveLife();
        void Reset();
    }
}