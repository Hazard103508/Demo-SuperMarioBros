using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "KoopaProfile", menuName = "ScriptableObjects/Game/Npc/KoopaProfile", order = 1)]
    public class KoopaProfile : ScriptableObject
    {
        public LayerMask GroundLayer;
        public float MoveSpeed;
        public float FallSpeed;
        public float MaxFallSpeed;
        public float JumpAcceleration;
        public int PointsHit;
        public int PointsKill;
    }
}