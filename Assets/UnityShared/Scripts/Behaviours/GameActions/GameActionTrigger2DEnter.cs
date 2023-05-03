using UnityEngine;

namespace UnityShared.Behaviours.GameActions
{
    [DisallowMultipleComponent]
    public class GameActionTrigger2DEnter : GameActionTrigger2D
    {
        private void OnTriggerEnter2D(Collider2D collision) => OnDetection(collision);
    }
}