using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;

namespace Mario.Game.Player
{
    public class SquareRaycast : MonoBehaviour
    {
        [SerializeField] private RaycastRange _bottomRayCast;
        [SerializeField] private RaycastRange _leftRayCast;
        [SerializeField] private RaycastRange _rightRayCast;
        [SerializeField] private RaycastRange _topRayCast;

        public void CalculateCollision()
        {
            if (_bottomRayCast != null)
                _bottomRayCast.CalculateCollision();

            if (_topRayCast != null)
                _topRayCast.CalculateCollision();

            if (_leftRayCast != null)
                _leftRayCast.CalculateCollision();

            if (_rightRayCast != null)
                _rightRayCast.CalculateCollision();
        }
    }
}