using Mario.Game.Handlers;
using UnityEngine;

namespace Mario.Game.Rewards
{
    public class JumpingCoin : Reward
    {
        private void OnEnable()
        {
            GameDataHandler.Instance.IncreaseScore(Profile.Points);
        }
        public void OnJumpCompleted()
        {
            GameDataHandler.Instance.ShowPoint(Profile.Points, transform.position + new Vector3(0, 1.5f, 0));
            Destroy(gameObject);
        }
    }
}