using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxCoinProfile", order = 2)]
    public class MysteryBoxCoinProfile : ScriptableObject
    {
        public PooledObjectProfile CoinPoolReference;
    }
}