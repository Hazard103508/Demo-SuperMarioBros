using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        public string PlayerName;
        public LayerMask GroundLayer;
        public HorizontalMovement Walk;
        public HorizontalMovement Run;
        public VerticalMovement Fall;
        public JumpMovement Jump;
        public PlayerSizes sizes;

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
        [Serializable]
        public class JumpMovement
        {
            public float Acceleration;
            public float MaxSpeed;
            public float MinBufferTime;
            public float MaxWalkBufferTime;
            public float MaxRunBufferTime;
            public float HorizontalAdjustmentSpeed;
        }
        [Serializable]
        public class PlayerSizes
        {
            public Size2 small;
            public Size2 big;
        }
    }
}