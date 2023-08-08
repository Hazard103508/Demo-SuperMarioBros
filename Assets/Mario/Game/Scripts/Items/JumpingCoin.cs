using Mario.Application.Components;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Items;
using UnityEngine;

namespace Mario.Game.Items
{
    public class JumpingCoin : MonoBehaviour
    {
        #region Objects
        [SerializeField] private CoinProfile _profile;
        #endregion

        #region Unity Methods
        private void OnEnable()
        {
            Services.ScoreService.Add(_profile.Points);
            Services.CoinService.Add();
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