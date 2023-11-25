using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "ElevatorProfile", menuName = "ScriptableObjects/Game/Interactable/ElevatorProfile", order = 0)]
    public class ElevatorProfile : ScriptableObject
    {
        [SerializeField] private float _speed;

        public float Speed => _speed;
    }
}