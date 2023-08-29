using System;
using UnityEditor.Animations;
using UnityEngine;
using UnityShared.ScriptableObjects.GameObjects;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerMode", menuName = "ScriptableObjects/Game/Player/PlayerMode", order = 1)]
    public class PlayerMode : ScriptableObject
    {
        public AnimatorController AnimatorController;
        public ModeRaycastRange RaycastRange;

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