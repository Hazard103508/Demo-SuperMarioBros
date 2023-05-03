using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTrigger2DStay : GameActionTrigger2D
    {
        private void OnTriggerStay2D(Collider2D collision) => OnDetection(collision);
    }
}