using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxEmptyProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxEmptyProfile", order = 1)]
    public class BrickBoxEmptyProfile : BoxProfile
    {
        [SerializeField] private PooledObjectProfile _brokenBrickPoolReference;
        [SerializeField] private PooledSoundProfile _breakSoundFXPoolReference;
        [SerializeField] private int _points;

        public PooledObjectProfile BrokenBrickPoolReference => _brokenBrickPoolReference;
        public PooledSoundProfile BreakSoundFXPoolReference => _breakSoundFXPoolReference;
        public int Points => _points;
    }
}