using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "PlantProfile", menuName = "ScriptableObjects/Game/Npc/PlantProfile", order = 0)]
    public class PlantProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _kickSoundFXPoolReference;
        [SerializeField] private float _timeVisible;
        [SerializeField] private float _timeHiden;
        [SerializeField] private int _pointsKill;

        public PooledSoundProfile KickSoundFXPoolReference => _kickSoundFXPoolReference;
        public float TimeVisible => _timeVisible;
        public float TimeHiden => _timeHiden;
        public int PointsKill => _pointsKill;
    }
}