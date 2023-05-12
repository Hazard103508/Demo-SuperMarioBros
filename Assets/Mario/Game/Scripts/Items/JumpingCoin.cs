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
            GameDataHandler.Instance.IncreaseScore(_profile.Points);
            GameDataHandler.Instance.IncreaseCoin(1);
        }
        public void OnJumpCompleted()
        {
            GameDataHandler.Instance.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            Destroy(gameObject);
        }
    }
}