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
        bool IsAutowalk { get; }

        event Action LivesAdded;
        event Action LivesRemoved;

        void SetPlayer(PlayerController playerController);
        void SetPlayerPosition(Vector3 position);
        void SetActivePlayer(bool isActive);
        void EnableAutoWalk(bool enable);
        void EnableInputs(bool enable);
        void ResetState();
        void TranslatePlayerPosition(Vector3 position);
        void KillPlayer();
        void KillPlayerByTimeOut();
        void AddLife();
        void RemoveLife();
        void Reset();
        void ShootFireball();
        bool IsPlayerSmall();
    }
}