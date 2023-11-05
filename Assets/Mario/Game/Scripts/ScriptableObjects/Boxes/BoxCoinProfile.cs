using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/BoxCoinProfile", order = 3)]
    public class BoxCoinProfile : BoxProfile
    {
        [SerializeField] private PooledObjectProfile _coinPoolReference;

        public PooledObjectProfile CoinPoolReference => _coinPoolReference;
    }
}