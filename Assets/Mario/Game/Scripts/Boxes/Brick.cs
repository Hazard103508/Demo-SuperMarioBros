using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class Brick : BottomHitableBlock
    {
        [SerializeField] private BrickProfile _brickProfile;

        public override void OnHitFromBottom(PlayerController player)
        {
            if (player.RawMovement.y > 0)
            {
                if (player.Mode == Enums.PlayerModes.Small)
                    base.OnHitFromBottom(player);
                else
                {
                    InstantiateContent(_brickProfile.BrokenBrick);
                    AllServices.ScoreService.Add(_brickProfile.Points);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (player.IsDucking)
                    return;

                player.IsStuck = true;
            }
        }

        public override void OnJumpCompleted() => IsHitable = true;
    }
}