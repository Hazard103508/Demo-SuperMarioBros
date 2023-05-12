using Mario.Game.Enums;
using Mario.Game.Handlers;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        public override void OnHitLeft(PlayerController player) => CollectMushrom(player);
        public override void OnHitBottom(PlayerController player) => CollectMushrom(player);
        public override void OnHitRight(PlayerController player) => CollectMushrom(player);
        public override void OnHitTop(PlayerController player) => CollectMushrom(player);

        private void CollectMushrom(PlayerController player)
        {
            GameDataHandler.Instance.IncreaseScore(_profile.Points);
            GameDataHandler.Instance.ShowPoint(_profile.Points, this.transform.position + Vector3.up);
            player.Mode = PlayerModes.Big;
            Destroy(gameObject);
        }
    }
}