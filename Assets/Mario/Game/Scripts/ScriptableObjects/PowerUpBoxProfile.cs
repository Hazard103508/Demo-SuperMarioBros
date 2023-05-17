using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PowerUpBoxProfile", menuName = "ScriptableObjects/Game/PowerUpBoxProfile", order = 10)]
    public class PowerUpBoxProfile : ScriptableObject
    {
        public GameObject RedMushroom;
        public GameObject Flower;
    }
}