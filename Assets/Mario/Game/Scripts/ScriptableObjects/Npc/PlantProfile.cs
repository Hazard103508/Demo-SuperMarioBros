using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "PlantProfile", menuName = "ScriptableObjects/Game/Npc/PlantProfile", order = 0)]
    public class PlantProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _hitSoundFXPoolReference;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _timeVisible;
        [SerializeField] private float _timeHiden;
        [SerializeField] private int _points;

        public PooledSoundProfile HitSoundFXPoolReference => _hitSoundFXPoolReference;
        public float MoveSpeed => _moveSpeed;
        public float TimeVisible => _timeVisible;
        public float TimeHiden => _timeHiden;
        public int Points => _points;
    }
}