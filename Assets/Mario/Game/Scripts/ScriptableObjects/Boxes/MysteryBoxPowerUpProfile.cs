using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxPowerUpProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxPowerUpProfile", order = 4)]
    public class MysteryBoxPowerUpProfile : BoxProfile
    {
        public PooledObjectProfile RiseItemSoundFXPoolReference;
        public PooledObjectProfile MushroomPoolReference;
        public PooledObjectProfile FlowerPoolReference;
    }
}