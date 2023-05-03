using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTriggerStay : GameActionTrigger
    {
        private void OnTriggerStay(Collider collision) => OnDetection(collision);
    }
}