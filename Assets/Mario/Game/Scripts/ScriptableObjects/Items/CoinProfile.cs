using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "CoinProfile", menuName = "ScriptableObjects/Game/Items/CoinProfile", order = 0)]
    public class CoinProfile : ScriptableObject
    {
        public string CoinPoolReference;
        public int Points;
    }
}