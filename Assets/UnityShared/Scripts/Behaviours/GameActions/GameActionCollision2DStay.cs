using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollision2DStay : GameActionCollision2D
    {
        private void OnCollisionStay2D(Collision2D collision) => OnDetection(collision);
    }
}