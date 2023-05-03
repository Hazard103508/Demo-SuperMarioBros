using UnityEngine;

namespace UnityShared.ScriptableObjects.GameObjects
{
    [CreateAssetMenu(fileName = "BreakableProfile", menuName = "ScriptableObjects/GameObjects/Breakable", order = 0)]
    public class BreakableProfile : ScriptableObject
    {
        public GameObject replacement;
        public float breakForce;
        public float collisionMultiplier;

    }
}