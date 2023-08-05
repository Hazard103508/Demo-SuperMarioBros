using UnityEngine;

namespace Mario.Game.Interfaces
{
    public interface IHitableByBox
    {
        void OnHittedByBox(GameObject box);
    }
}