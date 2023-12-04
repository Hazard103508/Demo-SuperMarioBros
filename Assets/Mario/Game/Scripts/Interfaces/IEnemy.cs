using UnityEngine;

namespace Mario.Game.Interfaces
{
    public interface IEnemy
    {
        void Kill(Vector3 hitPosition);
    }
}