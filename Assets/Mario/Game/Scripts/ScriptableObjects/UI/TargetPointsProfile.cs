using Mario.Game.Commons;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "TargetPointsProfile", menuName = "ScriptableObjects/Game/UI/TargetPointsProfile", order = 1)]
    public class TargetPointsProfile : ScriptableObject
    {
        [SerializeField] private NumberSprite[] _numberSprites;

        public Dictionary<char, Sprite> Sprites { get; private set; }

        private void OnEnable()
        {
            Sprites = new Dictionary<char, Sprite>();
            foreach (var item in _numberSprites)
                Sprites.Add(item.Value, item.Sprite);
        }
    }
}
