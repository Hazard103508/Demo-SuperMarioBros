using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickBoxEmptyProfile", menuName = "ScriptableObjects/Game/Boxes/BrickBoxEmptyProfile", order = 0)]
    public class BrickBoxEmptyProfile : ScriptableObject
    {
        public GameObject BrokenBrick;
        public int Points;
    }
}