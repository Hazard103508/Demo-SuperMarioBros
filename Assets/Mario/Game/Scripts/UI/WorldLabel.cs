using Mario.Application.Components;
using Mario.Commons.UI;
using UnityEngine;

namespace Mario.Game.UI
{
    public class WorldLabel : ObjectPool
    {
        [SerializeField] private IconText _label;
        public string Text { get => _label.Text; set => _label.Text = value; }
    }
}