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

        void SetPlayer(PlayerController playerController);
        void SetPlayerEnabled(bool isActive);
        void SetPlayerPosition(Vector3 position);
        void KillPlayer();
        void KillPlayerByTimeOut();
        void AddLife();
        void RemoveLife();
        void Reset();
    }
}