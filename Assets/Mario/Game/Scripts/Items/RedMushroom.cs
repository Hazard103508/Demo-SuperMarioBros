using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class RedMushroom : Mushroom
    {
        [SerializeField] private RedMushroomProfile _redMushroomProfile;
        private bool isCollected;

        protected override void Awake()
        {
            base.Awake();
        }

        public override void OnHitFromLeft(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromBottom(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromRight(PlayerController player) => CollectMushroom(player);
        public override void OnHitFromTop(PlayerController player) => CollectMushroom(player);

        private void CollectMushroom(PlayerController player)
        {
            if (isCollected || IsRising)
                return;

            isCollected = true;
            AllServices.ScoreService.Add(_redMushroomProfile.Points);
            AllServices.ScoreService.ShowPoint(_redMushroomProfile.Points, transform.position + Vector3.up * 1.25f, 0.8f, 3f);

            player.Buff();
            Destroy(gameObject);
        }
    }
}