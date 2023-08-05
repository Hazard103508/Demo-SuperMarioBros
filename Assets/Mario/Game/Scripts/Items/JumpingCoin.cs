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
        protected void OnEnable()
        {
            base.OnEnable();
            AllServices.ScoreService.Add(_profile.Points);
            AllServices.CoinService.Add();
        }
        #endregion

        #region Public Methods
        public void OnJumpCompleted()
        {
            AllServices.ScoreService.ShowPoint(_profile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 1.5f);
            gameObject.SetActive(false);
        }
        #endregion
    }
}