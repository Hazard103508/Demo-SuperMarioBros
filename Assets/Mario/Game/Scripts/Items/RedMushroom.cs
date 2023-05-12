using Mario.Game.Enums;
using Mario.Game.Handlers;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.Profiling;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        public override void HitLeft(PlayerController player)
        {
            GameDataHandler.Instance.IncreaseScore(_profile.Points);
            player.Mode = PlayerModes.Big;
            Destroy(gameObject);
        }
    }
}