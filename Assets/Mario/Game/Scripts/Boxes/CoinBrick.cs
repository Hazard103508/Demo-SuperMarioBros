using Mario.Game.Player;
using Mario.Game.ScriptableObjects;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class CoinBrick : BottomHitableBlock
    {
        [SerializeField] private CoinBrickProfile  _coinBrickProfile;
        [SerializeField] private Animator _spriteAnimator;
        private float _limitTime;

        private bool _firstHit;
        private bool _isEmpty;

        private void Start()
        {
            _limitTime = _coinBrickProfile.LimitTime;
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            _firstHit = true;

            base.OnHitFromBottom(player);

            if (_limitTime < 0)
            {
                _isEmpty = true;
                _spriteAnimator.SetTrigger("Disable");
            }

            base.InstantiateContent(_coinBrickProfile.CoinPrefab);
        }
        public override void OnJumpCompleted()
        {
            IsHitable = !_isEmpty;
        }
        private void Update()
        {
            if (_limitTime > 0 && _firstHit)
                _limitTime -= Time.deltaTime;
        }
    }
}