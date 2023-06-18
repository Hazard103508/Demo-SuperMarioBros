using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "GoombaProfile", menuName = "ScriptableObjects/Game/Npc/GoombaProfile", order = 0)]
    public class GoombaProfile : ScriptableObject
    {
        public LayerMask GroundLayer;
        public float MoveSpeed;
        public float FallSpeed;
        public float MaxFallSpeed;
        public int Points;
    }
}