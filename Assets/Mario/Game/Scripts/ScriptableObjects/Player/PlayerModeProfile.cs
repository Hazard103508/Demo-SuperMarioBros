using Mario.Game.ScriptableObjects.Pool;
using System;
using UnityEngine;
using UnityShared.Commons.Structs;
using UnityShared.ScriptableObjects.GameObjects;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "PlayerMode", menuName = "ScriptableObjects/Game/Player/PlayerMode", order = 1)]
    public class PlayerModeProfile : ScriptableObject
    {
        [SerializeField] private RuntimeAnimatorController _animatorController;
        [SerializeField] private RuntimeAnimatorController _starAnimatorController;
        [SerializeField] private HorizontalMovement _walk;
        [SerializeField] private HorizontalMovement _run;
        [SerializeField] private VerticalMovement _fall;
        [SerializeField] private JumpMovement _jump;
        [SerializeField] private ModeRaycastRange _normalRaycastRange;
        [SerializeField] private ModeRaycastRange _duckingRaycastRange;
        [SerializeField] private ModeCollider _collider;

        public RuntimeAnimatorController AnimatorController => _animatorController;
        public RuntimeAnimatorController StarAnimatorController => _starAnimatorController;
        public HorizontalMovement Walk => _walk;
        public HorizontalMovement Run => _run;
        public VerticalMovement Fall => _fall;
        public JumpMovement Jump => _jump;
        public ModeRaycastRange NormalRaycastRange => _normalRaycastRange;
        public ModeRaycastRange DuckingRaycastRange => _duckingRaycastRange;
        public ModeCollider Collider => _collider;

        [Serializable]
        public class HorizontalMovement
        {
            [SerializeField] private float _acceleration;
            [SerializeField] private float _maxSpeed;
            [SerializeField] private float _deacceleration;

            public float Acceleration => _acceleration;
            public float MaxSpeed => _maxSpeed;
            public float Deacceleration => _deacceleration;
        }
        [Serializable]
        public class VerticalMovement
        {
            [SerializeField] private float _normalSpeed;
            [SerializeField] private float _deathFallSpeed;
            [SerializeField] private float _maxFallSpeed;

            public float NormalSpeed => _normalSpeed;
            public float DeathFallSpeed => _deathFallSpeed;
            public float MaxFallSpeed => _maxFallSpeed;
        }
        [Serializable]
        public class JumpMovement
        {
            [SerializeField] private RangeNumber<float> _walkHeight;
            [SerializeField] private RangeNumber<float> _runHeight;
            [SerializeField] private float _deathHeight;
            [SerializeField] private float _bounce;
            [SerializeField] private PooledSoundProfile _soundFX;

            public RangeNumber<float> WalkHeight => _walkHeight;
            public RangeNumber<float> RunHeight => _runHeight;
            public float DeathHeight => _deathHeight;
            public float Bounce => _bounce;
            public PooledSoundProfile SoundFX => _soundFX;
        }
        [Serializable]
        public class ModeRaycastRange
        {
            [SerializeField] private RaycastRangeProfile _top;
            [SerializeField] private RaycastRangeProfile _bottom;
            [SerializeField] private RaycastRangeProfile _left;
            [SerializeField] private RaycastRangeProfile _right;

            public RaycastRangeProfile Top => _top;
            public RaycastRangeProfile Bottom => _bottom;
            public RaycastRangeProfile Left => _left;
            public RaycastRangeProfile Right => _right;
        }
        [Serializable]
        public class ModeCollider
        {
            [SerializeField] private Vector2 _offSet;
            [SerializeField] private Vector2 _size;

            public Vector2 Offset => _offSet;
            public Vector2 Size => _size;
        }
    }
}