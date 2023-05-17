using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BrokenBrick : MonoBehaviour
    {
        public void OnAnimationCompleted() => Destroy(gameObject);
    }
}