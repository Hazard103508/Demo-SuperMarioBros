using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "FlowerProfile", menuName = "ScriptableObjects/Game/Items/FlowerProfile", order = 1)]
    public class FlowerProfile : ScriptableObject
    {
        [SerializeField] private int _points;

        public int Points => _points;
    }
}