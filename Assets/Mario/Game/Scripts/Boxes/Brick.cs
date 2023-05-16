using Mario.Game.Player;

namespace Mario.Game.Boxes
{
    public class Brick : BottomHitableBlock
    {
        public override void OnHitFromBottom(PlayerController player)
        {
            if (player.Mode == Enums.PlayerModes.Small)
                base.OnHitFromBottom(player);
            else
            {
                InstantiateContent();
                Destroy(gameObject);
            }
        }

        public override void OnJumpCompleted() => IsHitable = true;
    }
}