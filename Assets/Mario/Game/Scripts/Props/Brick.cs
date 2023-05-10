using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Props
{
    public class Brick : MonoBehaviour, ITopHitable
    {
        private Animator _animator;
        private bool _isHitable;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _isHitable = true;
        }
        public void HitTop(PlayerController player)
        {
            if (!_isHitable)
                return;

            _animator.SetTrigger("Jump");
            _isHitable = false;
        }
        public void OnJumpCompleted() => _isHitable = true;
    }
}