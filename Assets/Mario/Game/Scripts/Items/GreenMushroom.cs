using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class GreenMushroom : Mushroom
    {
        [SerializeField] private GreenMushroomProfile _greenMushroomProfile;
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
            AllServices.LifeService.Add();
            AllServices.ScoreService.ShowLabel(_greenMushroomProfile.Sprite1UP, transform.position + Vector3.up * 1.25f, 0.8f, 3f);
            
            Destroy(gameObject);
        }
    }
}