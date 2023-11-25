using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBox1UPProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBox1UPProfile", order = 5)]
    public class BrickBox1UPProfile : BoxProfile
    {
        [SerializeField] private PooledSoundProfile _riseItemSoundFXPoolReference;
        [SerializeField] private PooledObjectProfile _mushroomPoolReference;

        public PooledSoundProfile RiseItemSoundFXPoolReference => _riseItemSoundFXPoolReference;
        public PooledObjectProfile MushroomPoolReference => _mushroomPoolReference;
    }
}