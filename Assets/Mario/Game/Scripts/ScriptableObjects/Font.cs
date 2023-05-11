using Mario.Game.Commons;
using System.Collections.Generic;
using UnityEngine;
using static Mario.Game.ScriptableObjects.TargetPointsProfile;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Font", menuName = "ScriptableObjects/Game/Font", order = 3)]
    public class Font : ScriptableObject
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