using Mario.Application.Components;

namespace Mario.Game.Items
{
    public class BrokenBrick : ObjectPool
    {
        public void OnAnimationCompleted() => gameObject.SetActive(false);
    }
}