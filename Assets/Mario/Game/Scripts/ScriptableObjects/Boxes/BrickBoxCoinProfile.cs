using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxCoinProfile", order = 1)]
    public class BrickBoxCoinProfile : ScriptableObject
    {
        public GameObject CoinPrefab;
        public float LimitTime;
    }
}