using Mario.Application.Interfaces;
using Mario.Game.Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ItemsService : MonoBehaviour, IItemsService
    {
        public void LoadService()
        {
            CanMove = true;
        }

        public bool CanMove { get; private set; }

        public void StopMovement() => CanMove = false;
        public void ResumeMovement() => CanMove = true;
    }
}