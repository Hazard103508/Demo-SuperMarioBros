using Mario.Game.Handlers;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : MonoBehaviour
    {
        [SerializeField] private CoinProfile _coinProfile;

        private void OnEnable()
        {
            GameDataHandler.Instance.IncreaseScore(_coinProfile.Points);
            GameDataHandler.Instance.IncreaseCoin(1);
        }
        public void OnJumpCompleted()
        {
            GameDataHandler.Instance.ShowPoint(_coinProfile.Points, transform.position + new Vector3(0, 1.5f, 0));
            Destroy(gameObject);
        }
    }
}