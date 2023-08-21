using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxCoinProfile", order = 2)]
    public class BrickBoxCoinProfile : BoxProfile
    {
        public PooledObjectProfile CoinPoolReference;
        public float LimitTime;
    }
}