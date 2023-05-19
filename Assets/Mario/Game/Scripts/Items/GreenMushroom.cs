using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Items
{
    public class GreenMushroom : Mushroom
    {
        [SerializeField] private Sprite Label1UP;
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
            //AllServices.ScoreService.Add(_mushroomProfile.Points);
            AllServices.ScoreService.ShowLabel(Label1UP, transform.position + Vector3.up * 1.25f, 0.8f, 3f);
            
            Destroy(gameObject);
        }
    }
}