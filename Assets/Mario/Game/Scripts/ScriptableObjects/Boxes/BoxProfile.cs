using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BoxProfile", menuName = "ScriptableObjects/Game/Boxes/BoxProfile", order = 0)]
    public class BoxProfile : ScriptableObject
    {
        public LayerMask TargetLayer;
        public PooledObjectProfile HitSoundFXPoolReference;
        public float FallSpeed;
        public float MaxFallSpeed;
        public float JumpAcceleration;
    }
}