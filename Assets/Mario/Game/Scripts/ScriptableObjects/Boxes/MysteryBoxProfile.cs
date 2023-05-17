using UnityEngine;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxProfile", menuName = "ScriptableObjects/Game/MysteryBoxProfile", order = 9)]
    public class MysteryBoxProfile : ScriptableObject
    {
        public GameObject Item;
        public bool InstantiateItemOnHit;
    }
}