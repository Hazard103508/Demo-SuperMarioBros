using UnityEngine;

namespace Mario.Game.Interfaces
{
    public interface IHittableByBox
    {
        void OnHittedByBox(GameObject box);
    }
}