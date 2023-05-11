using UnityEngine;

namespace Mario.Game.Props
{
    public class JumpingCoin : MonoBehaviour
    {
        public void OnJumpCompleted()
        {
            Destroy(gameObject);
        }
    }
}