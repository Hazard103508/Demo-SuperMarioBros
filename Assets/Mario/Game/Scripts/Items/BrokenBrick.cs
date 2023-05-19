using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Items
{
    public class BrokenBrick : MonoBehaviour
    {
        public void OnAnimationCompleted() => Destroy(gameObject);
    }
}