using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollisionExit : GameActionCollision
    {
        private void OnCollisionExit(Collision collision) => OnDetection(collision);
    }
}