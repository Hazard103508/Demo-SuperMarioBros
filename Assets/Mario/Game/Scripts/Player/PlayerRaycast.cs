using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;

namespace Mario.Game.Player
{
    public class PlayerRaycast : MonoBehaviour
    {
        [SerializeField] private RaycastRange _bottomRayCast;
        [SerializeField] private RaycastRange _leftRayCast;
        [SerializeField] private RaycastRange _rightRayCast;
        [SerializeField] private RaycastRange _topRayCast;

        public void CalculateCollision()
        {
            _bottomRayCast.CalculateCollision();
            _topRayCast.CalculateCollision();
            _leftRayCast.CalculateCollision();
            _rightRayCast.CalculateCollision();
        }
    }
}