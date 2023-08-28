using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;
using UnityShared.ScriptableObjects.GameObjects;

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
        public PlayerFireball Fireball;

        [Header("Raycast")]
        public ModeRaycastRange RaycastSmall;
        public ModeRaycastRange RaycastBig;


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
            public float MinHeight;
            public float MaxIdleHeight;
            public float MaxRunHeight;
        }
        [Serializable]
        public class PlayerSpritePositions
        {
            public Vector2 Small;
            public Vector2 Big;
        }
        [Serializable]
        public class PlayerFireball
        {
            public Vector2 StartLocalPosition;
            public PooledObjectProfile FireballPoolProfile;
        }
        [Serializable]
        public class ModeRaycastRange
        {
            public RaycastRangeProfile Top;
            public RaycastRangeProfile Bottom;
            public RaycastRangeProfile Left;
            public RaycastRangeProfile Right;
        }
    }
}