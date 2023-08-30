using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityShared.ScriptableObjects.GameObjects;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerMode", menuName = "ScriptableObjects/Game/Player/PlayerMode", order = 1)]
    public class PlayerModeProfile : ScriptableObject
    {
        public AnimatorController AnimatorController;
        public HorizontalMovement Walk;
        public HorizontalMovement Run;
        public VerticalMovement Fall;
        public JumpMovement Jump;
        public ModeRaycastRange RaycastRange;

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
            public float DeathFallSpeed;
            public float MaxFallSpeed;
        }
        [Serializable]
        public class JumpMovement
        {
            public float MinHeight;
            public float MaxIdleHeight;
            public float MaxRunHeight;
            public float DeathHeight;
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