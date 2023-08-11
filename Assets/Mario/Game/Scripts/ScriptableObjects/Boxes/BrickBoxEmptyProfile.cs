using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxEmptyProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxEmptyProfile", order = 0)]
    public class BrickBoxEmptyProfile : ScriptableObject
    {
        public PooledObjectProfile BrokenBrickPoolReference;
        public int Points;
    }
}