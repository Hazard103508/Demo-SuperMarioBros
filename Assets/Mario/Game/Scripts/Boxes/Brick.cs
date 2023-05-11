namespace Mario.Game.Boxes
{
    public class Brick : TopHitableBlock
    {
        public override void OnJumpCompleted() => IsHitable = true;
    }
}