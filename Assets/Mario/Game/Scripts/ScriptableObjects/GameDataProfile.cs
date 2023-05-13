using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameDataProfile", menuName = "ScriptableObjects/Game/GameDataProfile", order = 99)]
    public class GameDataProfile : ScriptableObject
    {
        public WorldMapProfile WorldMapProfile;
        [SerializeField] private string _player;



        public string Player => _player;
    }
}