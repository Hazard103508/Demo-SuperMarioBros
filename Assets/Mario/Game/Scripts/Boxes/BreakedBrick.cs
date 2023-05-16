using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BreakedBrick : MonoBehaviour
    {
        public void OnAnimationCompleted() => Destroy(gameObject);
    }
}