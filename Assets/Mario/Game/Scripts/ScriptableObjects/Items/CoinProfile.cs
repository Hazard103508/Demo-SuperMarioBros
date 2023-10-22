using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "CoinProfile", menuName = "ScriptableObjects/Game/Items/CoinProfile", order = 0)]
    public class CoinProfile : ScriptableObject
    {
        [SerializeField] private PooledObjectProfile _coinPoolReference;
        [SerializeField] private int _points;

        public PooledObjectProfile CoinPoolReference => _coinPoolReference;
        public int Points => _points;
    }
}