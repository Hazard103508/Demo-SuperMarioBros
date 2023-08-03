using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes
{
    public class BrickBoxCoin : BottomHitableBox
    {
        [SerializeField] private BrickBoxCoinProfile _coinBrickProfile;
        [SerializeField] private Animator _spriteAnimator;
        private float _limitTime;

        private bool _firstHit;
        private bool _isEmpty;

        protected override void Awake()
        {
            base.Awake();
            _limitTime = _coinBrickProfile.LimitTime;

            AllServices.SceneService.AddAsset(_coinBrickProfile.CoinReference);
        }
        private void Update()
        {
            if (_limitTime > 0 && _firstHit)
                _limitTime -= Time.deltaTime;
        }

        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            PlayHitSoundFX();
            _firstHit = true;

            base.OnHitFromBottom(player);

            if (_limitTime < 0)
            {
                _isEmpty = true;
                _spriteAnimator.SetTrigger("LastHit");
            }
            else
                _spriteAnimator.SetTrigger("Hit");

            var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_coinBrickProfile.CoinReference);
            base.InstantiateContent(prefab);
        }
        public override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            IsHitable = !_isEmpty;

            _spriteAnimator.SetTrigger(_isEmpty ? "Disable" : "Idle");
        }
    }
}