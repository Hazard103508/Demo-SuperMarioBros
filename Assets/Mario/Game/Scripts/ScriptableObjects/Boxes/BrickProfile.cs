using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "BrickProfile", menuName = "ScriptableObjects/Game/BrickProfile", order = 6)]
    public class BrickProfile : ScriptableObject
    {
        public GameObject BrokenBrick;
        public int Points;
    }
}