using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RewardProfile", menuName = "ScriptableObjects/Game/RewardProfile", order = 3)]
    public class RewardProfile : ScriptableObject
    {
        public int Points;
    }
}
