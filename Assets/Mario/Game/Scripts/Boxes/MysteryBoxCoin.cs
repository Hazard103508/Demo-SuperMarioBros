using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class MysteryBoxCoin : BottomHitableBox
    {
        [SerializeField] protected MysteryBoxCoinProfile _mysteryBoxProfile;
        [SerializeField] private Animator _spriteAnimator;

        protected override void Awake()
        {
            base.Awake();
            AllServices.SceneService.AddAsset(_mysteryBoxProfile.CoinReference);
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");

            var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_mysteryBoxProfile.CoinReference);
            base.InstantiateContent(prefab);
        }
    }
}