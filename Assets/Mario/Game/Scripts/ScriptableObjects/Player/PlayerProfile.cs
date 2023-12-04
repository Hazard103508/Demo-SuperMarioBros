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
        [SerializeField] private PlayerStarData _star;


        public string PlayerName => _playerName;
        public PlayerModes Modes => _modes;
        public PlayerFireball Fireball => _fireball;
        public PlayerStarData Star => _star;


        [Serializable]
        public class PlayerFireball
        {
            [SerializeField] private Vector2 _startLocalPosition;
            [SerializeField] private PooledObjectProfile _fireballPoolProfile;

            public Vector2 StartLocalPosition => _startLocalPosition;
            public PooledObjectProfile FireballPoolProfile => _fireballPoolProfile;
        }
        [Serializable]
        public class PlayerModes
        {
            [SerializeField] private PlayerModeProfile _small;
            [SerializeField] private PlayerModeProfile _big;
            [SerializeField] private PlayerModeProfile _super;

            public PlayerModeProfile Small => _small;
            public PlayerModeProfile Big => _big;
            public PlayerModeProfile Super => _super;
        }
        [Serializable]
        public class PlayerStarData
        {
            [SerializeField] private Material _material;
            [SerializeField] private PooledSoundProfile _soundFX;
            public Material Material => _material;
            public PooledSoundProfile SoundFX => _soundFX;
        }
    }
}