using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class Brick : BottomHitableBlock
    {
        [SerializeField] private BrickProfile _brickProfile;

        public override void OnHitFromBottom(PlayerController player)
        {
            if (player.Mode == Enums.PlayerModes.Small)
                base.OnHitFromBottom(player);
            else
            {
                InstantiateContent();
                AllServices.ScoreService.Add(_brickProfile.Points);
                Destroy(gameObject);
            }
        }

        public override void OnJumpCompleted() => IsHitable = true;
    }
}