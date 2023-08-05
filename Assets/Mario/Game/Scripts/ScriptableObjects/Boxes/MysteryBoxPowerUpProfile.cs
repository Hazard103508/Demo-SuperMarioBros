using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Mario.Game.ScriptableObjects.Boxes
{
    [CreateAssetMenu(fileName = "MysteryBoxPowerUpProfile", menuName = "ScriptableObjects/Game/Boxes/MysteryBoxPowerUpProfile", order = 3)]
    public class MysteryBoxPowerUpProfile : ScriptableObject
    {
        public string RedMushroomPoolReference;
        public string FlowerPoolReference;
    }
}