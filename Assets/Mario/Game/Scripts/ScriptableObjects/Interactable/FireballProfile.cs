using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "FireballProfile", menuName = "ScriptableObjects/Game/Interactable/FireballProfile", order = 0)]
    public class FireballProfile : ScriptableObject
    {
        public float Speed;
        public float BounceSpeed;
        public float FallSpeed;
        public float MaxFallSpeed;
        public PooledObjectProfile ExplotionPoolReference;
        public PooledObjectProfile HitSoundFXPoolReference;
    }
}