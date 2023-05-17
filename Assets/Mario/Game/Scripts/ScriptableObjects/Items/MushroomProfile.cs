using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "MushroomProfile", menuName = "ScriptableObjects/Game/MushroomProfile", order = 5)]
    public class MushroomProfile : ScriptableObject
    {
        public LayerMask GroundLayer;
        public float MoveSpeed;
        public float FallSpeed;
        public float MaxFallSpeed;
        public int Points;
    }
}