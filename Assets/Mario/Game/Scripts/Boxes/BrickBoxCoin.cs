using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

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
        public override void OnHitFromBottom(PlayerController player)
        {
            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            _firstHit = true;

            base.OnHitFromBottom(player);

            if (_limitTime < 0)
            {
                _isEmpty = true;
                _spriteAnimator.SetTrigger("Disable");
            }

            var prefab = AllServices.SceneService.GetAssetReference<GameObject>(_coinBrickProfile.CoinReference);
            base.InstantiateContent(prefab);
        }
        public override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            IsHitable = !_isEmpty;
        }
        private void Update()
        {
            if (_limitTime > 0 && _firstHit)
                _limitTime -= Time.deltaTime;
        }
    }
}