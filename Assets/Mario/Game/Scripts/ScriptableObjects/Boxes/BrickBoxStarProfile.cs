using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxStarProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxStarProfile", order = 5)]
    public class BrickBoxStarProfile : BoxProfile
    {
        [SerializeField] private PooledSoundProfile _riseItemSoundFXPoolReference;
        [SerializeField] private PooledObjectProfile _starPoolReference;

        public PooledSoundProfile RiseItemSoundFXPoolReference => _riseItemSoundFXPoolReference;
        public PooledObjectProfile StarPoolReference => _starPoolReference;
    }
}