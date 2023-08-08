using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/Player/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        public string PlayerName;
        public LayerMask GroundLayer;
        public HorizontalMovement Walk;
        public HorizontalMovement Run;
        public VerticalMovement Fall;
        public JumpMovement Jump;
        public PlayerSpritePositions SpritePositions;

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
            public float BounceSpeed;
            public float MinBufferTime;
            public float MaxWalkBufferTime;
            public float MaxRunBufferTime;
        }
        [Serializable]
        public class PlayerSpritePositions
        {
            public Vector2 Small;
            public Vector2 Big;
        }
    }
}