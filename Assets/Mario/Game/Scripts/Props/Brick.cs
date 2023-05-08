using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Props
{
    public class Brick : MonoBehaviour, ITopHitable
    {
        public void HitTop(PlayerController player)
        {
            print("Hola");
        }
    }
}