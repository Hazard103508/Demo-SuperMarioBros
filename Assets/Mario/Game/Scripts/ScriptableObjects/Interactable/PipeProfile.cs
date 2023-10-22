using Mario.Game.ScriptableObjects.Map;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Interactable
{
    [CreateAssetMenu(fileName = "PipeProfile", menuName = "ScriptableObjects/Game/Interactable/PipeProfile", order = 0)]
    public class PipeProfile : ScriptableObject
    {
        [SerializeField] private PooledSoundProfile _soundFXPoolReference;
        [SerializeField] private MapConnection _connection;

        public PooledSoundProfile SoundFXPoolReference => _soundFXPoolReference;
        public MapConnection Connection => _connection;
    }
}