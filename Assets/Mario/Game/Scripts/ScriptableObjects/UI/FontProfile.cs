using Mario.Game.Commons;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "FontProfile", menuName = "ScriptableObjects/Game/UI/FontProfile", order = 0)]
    public class FontProfile : ScriptableObject
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