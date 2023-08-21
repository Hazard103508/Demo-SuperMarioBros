using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxCoinProfile", order = 3)]
    public class MysteryBoxCoinProfile : BoxProfile
    {
        public PooledObjectProfile CoinPoolReference;
    }
}