using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollision2DExit : GameActionCollision2D
    {
        private void OnCollisionExit2D(Collision2D collision) => OnDetection(collision);
    }
}