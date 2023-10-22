using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxCoinProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxCoinProfile", order = 2)]
    public class BrickBoxCoinProfile : BoxProfile
    {
        [SerializeField] private PooledObjectProfile _coinPoolReference;
        [SerializeField] private float _limitTime;

        public PooledObjectProfile CoinPoolReference => _coinPoolReference;
        public float LimitTime => _limitTime;
    }
}