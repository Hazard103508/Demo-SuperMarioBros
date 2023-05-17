using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CoinBrickProfile", menuName = "ScriptableObjects/Game/CoinBrickProfile", order = 11)]
    public class CoinBrickProfile : ScriptableObject
    {
        public GameObject CoinPrefab;
        public float LimitTime;
    }
}