using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxCoinProfile", order = 3)]
    public class MysteryBoxCoinProfile : BoxProfile
    {
        [SerializeField] private PooledObjectProfile _coinPoolReference;

        public PooledObjectProfile CoinPoolReference => _coinPoolReference;
    }
}