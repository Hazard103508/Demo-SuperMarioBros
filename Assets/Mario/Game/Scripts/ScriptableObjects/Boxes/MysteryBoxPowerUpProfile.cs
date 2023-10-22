using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxPowerUpProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxPowerUpProfile", order = 4)]
    public class MysteryBoxPowerUpProfile : BoxProfile
    {
        [SerializeField] private PooledObjectProfile _riseItemSoundFXPoolReference;
        [SerializeField] private PooledObjectProfile _mushroomPoolReference;
        [SerializeField] private PooledObjectProfile _flowerPoolReference;

        public PooledObjectProfile RiseItemSoundFXPoolReference => _riseItemSoundFXPoolReference;
        public PooledObjectProfile MushroomPoolReference => _mushroomPoolReference;
        public PooledObjectProfile FlowerPoolReference => _flowerPoolReference;
    }
}