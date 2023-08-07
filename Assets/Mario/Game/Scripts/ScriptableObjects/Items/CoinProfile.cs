using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "CoinProfile", menuName = "ScriptableObjects/Game/Items/CoinProfile", order = 0)]
    public class CoinProfile : ScriptableObject
    {
        public ObjectPoolProfile CoinPoolReference;
        public int Points;
    }
}