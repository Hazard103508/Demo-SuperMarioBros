using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTriggerExit : GameActionTrigger
    {
        private void OnTriggerExit(Collider collision) => OnDetection(collision);
    }
}