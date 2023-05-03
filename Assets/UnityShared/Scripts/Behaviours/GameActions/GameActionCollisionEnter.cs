using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionCollisionEnter : GameActionCollision
    {
        private void OnCollisionEnter(Collision collision) => OnDetection(collision);
    }
}