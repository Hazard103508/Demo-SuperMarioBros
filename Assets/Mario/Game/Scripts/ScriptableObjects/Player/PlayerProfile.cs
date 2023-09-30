using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/Player/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        public string PlayerName;
        public LayerMask GroundLayer;
        public PlayerModes Modes;
        public PlayerFireball Fireball;
        public ModeChange Buff;
        public ModeChange Nerf;

        [Serializable]
        public class PlayerFireball
        {
            public Vector2 StartLocalPosition;
            public PooledObjectProfile FireballPoolProfile;
        }
        [Serializable]
        public class PlayerModes
        {
            public PlayerModeProfile Small;
            public PlayerModeProfile Big;
            public PlayerModeProfile Super;
        }
        [Serializable]
        public class ModeChange
        {
            public PooledSoundProfile SoundFX;
        }

    }
}