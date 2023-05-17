using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Map
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

        public PlayerRect SmallPlayer;
        public PlayerRect BigPlayer;
        public PlayerRect DuckingPlayer;

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
        public class PlayerRect
        {
            public Vector2 SpritePosition;
            public Rect Collider;
        }
    }
}