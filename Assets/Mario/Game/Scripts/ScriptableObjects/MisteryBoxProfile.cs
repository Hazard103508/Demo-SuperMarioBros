using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "MisteryBoxProfile", menuName = "ScriptableObjects/Game/MisteryBoxProfile", order = 1)]
    public class MisteryBoxProfile : ScriptableObject
    {
        public GameObject prefab;
    }
}