using Mario.Game.ScriptableObjects.Map;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.Items
{
    [CreateAssetMenu(fileName = "PipeProfile", menuName = "ScriptableObjects/Game/Interactable/PipeProfile", order = 1)]
    public class PipeProfile : ScriptableObject
    {
        public MapProfile MapProfile;
        public AudioClip SoundFX;
    }
}