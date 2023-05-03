using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollision2DEnter : GameActionCollision2D
    {
        private void OnCollisionEnter2D(Collision2D collision) => OnDetection(collision);
    }
}