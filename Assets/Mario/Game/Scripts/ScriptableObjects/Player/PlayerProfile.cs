using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/Player/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        [SerializeField] private string _playerName;
        [SerializeField] private PlayerModes _modes;
        [SerializeField] private PlayerFireball _fireball;
        [SerializeField] private ModeChange _buff;
        [SerializeField] private ModeChange _nerf;

        public string PlayerName => _playerName;
        public PlayerModes Modes => _modes;
        public ModeChange Buff => _buff;
        public ModeChange Nerf => _nerf;

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