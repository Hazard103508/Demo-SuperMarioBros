using Mario.Application.Services;
using Mario.Game.Enums;
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
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            if (player.Mode == PlayerModes.Small)
                player.Mode = PlayerModes.Big;
            Destroy(gameObject);
        }
    }
}