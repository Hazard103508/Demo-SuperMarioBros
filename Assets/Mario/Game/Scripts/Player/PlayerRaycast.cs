using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;

namespace Mario.Game.Player
{
    public class PlayerRaycast : MonoBehaviour
    {
        [SerializeField] private RaycastRange _bottomRayCast;

        public void CalculateBottomCollision() => _bottomRayCast.CalculateCollision();
    }
}