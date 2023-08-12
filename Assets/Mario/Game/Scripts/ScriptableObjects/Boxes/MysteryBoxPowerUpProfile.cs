using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxPowerUpProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxPowerUpProfile", order = 3)]
    public class MysteryBoxPowerUpProfile : ScriptableObject
    {
        public PooledObjectProfile RedMushroomPoolReference;
        public PooledObjectProfile FlowerPoolReference;
    }
}