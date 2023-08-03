using Mario.Game.Boxes;
using UnityEngine;

namespace Mario.Game.Interfaces
{
    public interface IBottomHitableByBox
    {
        void OnHitFromBottomByBox(GameObject box);
    }
}