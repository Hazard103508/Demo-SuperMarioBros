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
        private IScoreService _scoreService;
        private ICoinService _coinService;

        private void Awake()
        {
            _scoreService = ServiceLocator.Current.Get<IScoreService>();
            _coinService = ServiceLocator.Current.Get<ICoinService>();
        }
        private void OnEnable()
        {
            _scoreService.Add(_profile.Points);
            _coinService.Add();
        }
        public void OnJumpCompleted()
        {
            _scoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            Destroy(gameObject);
        }
    }
}