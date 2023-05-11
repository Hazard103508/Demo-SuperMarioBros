using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MysteryBoxProfile", menuName = "ScriptableObjects/Game/MysteryBoxProfile", order = 1)]
    public class MysteryBoxProfile : ScriptableObject
    {
        public GameObject Prefab;
        public int Points;
    }
}