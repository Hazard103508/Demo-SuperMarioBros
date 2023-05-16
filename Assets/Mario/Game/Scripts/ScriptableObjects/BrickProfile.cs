using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "BrickProfile", menuName = "ScriptableObjects/Game/BrickProfile", order = 6)]
    public class BrickProfile : ScriptableObject
    {
        public int Points;
    }
}