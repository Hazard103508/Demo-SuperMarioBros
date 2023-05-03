using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollisionStay : GameActionCollision
    {
        private void OnCollisionStay(Collision collision) => OnDetection(collision);
    }
}