using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "InvisibleBox1UPProfile", menuName = "ScriptableObjects/Game/Boxes/InvisibleBox1UPProfile", order = 5)]
    public class InvisibleBox1UPProfile : BoxProfile
    {
        [SerializeField] private PooledSoundProfile _riseItemSoundFXPoolReference;
        [SerializeField] private PooledObjectProfile _mushroomPoolReference;

        public PooledSoundProfile RiseItemSoundFXPoolReference => _riseItemSoundFXPoolReference;
        public PooledObjectProfile MushroomPoolReference => _mushroomPoolReference;
    }
}