using Mario.Game.Enums;
using Mario.Game.Handlers;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        private bool isCollected;

        public override void OnHitFromLeft(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromBottom(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromRight(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromTop(PlayerController player) => CollectMushroom(player);

        private void CollectMushroom(PlayerController player)
        {
            if (isCollected)
                return;

            isCollected = true;
            GameDataHandler.Instance.IncreaseScore(_profile.Points);
            GameDataHandler.Instance.ShowPoint(_profile.Points, this.transform.position + Vector3.up);
            player.Mode = PlayerModes.Big;
            Destroy(gameObject);
        }
    }
}