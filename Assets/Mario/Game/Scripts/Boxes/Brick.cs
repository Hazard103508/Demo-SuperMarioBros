namespace Mario.Game.Boxes
{
    public class Brick : BottomHitableBlock
    {
        public override void OnJumpCompleted() => IsHitable = true;
    }
}