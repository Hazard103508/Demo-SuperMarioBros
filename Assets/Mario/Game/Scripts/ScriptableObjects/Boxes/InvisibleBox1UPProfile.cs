using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "InvisibleBox1UPProfile", menuName = "ScriptableObjects/Game/Boxes/InvisibleBox1UPProfile", order = 5)]
    public class InvisibleBox1UPProfile : BoxProfile
    {
        public PooledObjectProfile GreenMushroomPoolReference;
    }
}