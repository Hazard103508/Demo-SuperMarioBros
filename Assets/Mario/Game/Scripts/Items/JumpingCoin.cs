using Mario.Game.Handlers;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : MonoBehaviour
    {
        [SerializeField] private CoinProfile _profile;

        private void OnEnable()
        {
            GameHandler.Instance.IncreaseScore(_profile.Points);
            GameHandler.Instance.IncreaseCoin(1);
        }
        public void OnJumpCompleted()
        {
            GameHandler.Instance.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            Destroy(gameObject);
        }
    }
}