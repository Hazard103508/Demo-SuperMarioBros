using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTrigger2DExit : GameActionTrigger2D
    {
        private void OnTriggerExit2D(Collider2D collision) => OnDetection(collision);
    }
}