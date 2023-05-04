using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        public LayerMask GroundLayer;
        public HorizontalMovement Walk;
        public HorizontalMovement Run;
        public VerticalMovement Fall;

        [Serializable]
        public class HorizontalMovement
        {
            public float Acceleration;
            public float MaxSpeed;
            public float Deacceleration;
        }
        [Serializable]
        public class VerticalMovement
        {
            public float FallSpeed;
            public float MaxFallSpeed;
        }
    }
}