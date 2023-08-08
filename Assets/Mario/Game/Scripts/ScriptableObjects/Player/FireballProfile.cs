using UnityEngine;

namespace Mario.Game.ScriptableObjects.Player
{
    [CreateAssetMenu(fileName = "FireballProfile", menuName = "ScriptableObjects/Game/Player/FireballProfile", order = 1)]
    public class FireballProfile : ScriptableObject
    {
        public float Speed;
        public float BounceSpeed;
        public float FallSpeed;
        public float MaxFallSpeed;
    }
}