using Mario.Application.Interfaces;
using Mario.Application.Services;
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
            ServiceLocator.Current.Get<IScoreService>().Add(_profile.Points);
            GameHandler.Instance.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);
            player.Mode = PlayerModes.Big;
            Destroy(gameObject);
        }
    }
}