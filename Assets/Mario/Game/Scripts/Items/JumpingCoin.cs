using Mario.Application.Interfaces;
using Mario.Application.Services;
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
            ServiceLocator.Current.Get<IScoreService>().Add(_profile.Points);
            ServiceLocator.Current.Get<ICoinService>().Add();
        }
        public void OnJumpCompleted()
        {
            GameHandler.Instance.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            Destroy(gameObject);
        }
    }
}