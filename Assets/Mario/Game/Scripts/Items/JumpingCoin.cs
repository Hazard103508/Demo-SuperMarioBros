using Mario.Application.Components;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : ObjectPool
    {
        #region Objects
        [SerializeField] private CoinProfile _profile;
        #endregion

        #region Unity Methods
        protected override void OnEnable()
        {
            base.OnEnable();
            Services.ScoreService.Add(_profile.Points);
            Services.CoinService.Add();
        }
        #endregion

        #region Public Methods
        public void OnJumpCompleted()
        {
            Services.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.8f, 1.5f);

            gameObject.SetActive(false);
        }
        #endregion
    }
}