namespace Mario.Game.Props
{
    public class Brick : TopHitableBlock
    {
        public override void OnJumpCompleted() => IsHitable = true;
    }
}