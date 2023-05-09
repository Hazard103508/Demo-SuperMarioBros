using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BrickProfile", menuName = "ScriptableObjects/Game/BrickProfile", order = 1)]
    public class BrickProfile : ScriptableObject
    {
        public float HitForce;
        public float FallSpeed;
    }
}