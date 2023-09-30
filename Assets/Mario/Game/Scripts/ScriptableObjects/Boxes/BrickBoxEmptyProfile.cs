using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxEmptyProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxEmptyProfile", order = 1)]
    public class BrickBoxEmptyProfile : BoxProfile
    {
        public PooledObjectProfile BrokenBrickPoolReference;
        public PooledSoundProfile BreakSoundFXPoolReference;
        public int Points;
    }
}