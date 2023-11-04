using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "HouseProfile", menuName = "ScriptableObjects/Game/Interactable/HouseProfile", order = 0)]
    public class HouseProfile : ScriptableObject
    {
        [SerializeField] private MapConnection _connection;

        public MapConnection Connection => _connection;
    }
}