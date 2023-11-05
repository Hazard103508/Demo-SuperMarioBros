using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BoxPowerUpProfile", menuName = "ScriptableObjects/Game/Boxes/BoxPowerUpProfile", order = 4)]
    public class BoxPowerUpProfile : BoxProfile
    {
        [SerializeField] private PooledSoundProfile _riseItemSoundFXPoolReference;
        [SerializeField] private PooledObjectProfile _mushroomPoolReference;
        [SerializeField] private PooledObjectProfile _flowerPoolReference;

        public PooledSoundProfile RiseItemSoundFXPoolReference => _riseItemSoundFXPoolReference;
        public PooledObjectProfile MushroomPoolReference => _mushroomPoolReference;
        public PooledObjectProfile FlowerPoolReference => _flowerPoolReference;
    }
}