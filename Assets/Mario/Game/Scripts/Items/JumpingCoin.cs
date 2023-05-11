using Mario.Game.Handlers;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : MonoBehaviour
    {
        public RewardProfile Profile;

        private void OnEnable()
        {
            GameDataHandler.Instance.IncreaseScore(Profile.Points);
            GameDataHandler.Instance.IncreaseCoin(1);
        }
        public void OnJumpCompleted()
        {
            GameDataHandler.Instance.ShowPoint(Profile.Points, transform.position + new Vector3(0, 1.5f, 0));
            Destroy(gameObject);
        }
    }
}