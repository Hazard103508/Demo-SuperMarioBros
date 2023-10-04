using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : MonoBehaviour
    {
        #region Objects
        private ICoinService _coinService;

        [SerializeField] private CoinProfile _profile;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
        }
        private void OnEnable()
        {
            Services.ScoreService.Add(_profile.Points);
            _coinService.Add();
        }
        #endregion

        #region Public Methods
        public void OnJumpCompleted()
        {
            Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.8f, 1.5f);
            gameObject.SetActive(false);
        }
        #endregion
    }
}