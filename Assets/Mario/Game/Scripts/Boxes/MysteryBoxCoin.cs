using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxCoin : Box
    {
        #region Objects
        [SerializeField] protected MysteryBoxCoinProfile _mysteryBoxProfile;
        [SerializeField] private Animator _spriteAnimator;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_mysteryBoxProfile.CoinReference);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();

            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_mysteryBoxProfile.CoinReference);
            base.InstantiateContent(prefab);
        }
        #endregion
    }
}