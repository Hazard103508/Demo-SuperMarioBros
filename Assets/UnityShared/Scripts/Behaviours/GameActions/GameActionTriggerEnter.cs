using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTriggerEnter : GameActionTrigger
    {
        private void OnTriggerEnter(Collider collision) => OnDetection(collision);
    }
}