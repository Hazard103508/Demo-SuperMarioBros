using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "CoinProfile", menuName = "ScriptableObjects/Game/CoinProfile", order = 5)]
    public class CoinProfile : ScriptableObject
    {
        public int Points;
    }
}