using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class BottomHitableBlock : MonoBehaviour, IBottomHitable
    {
        [SerializeField] private GameObject _contentPrefab;
        private Animator _boxAnimator;
        protected bool IsHitable { get; set; }

        private void Awake()
        {
            _boxAnimator = GetComponent<Animator>();
            IsHitable = true;
        }
        public virtual void OnHitFromBottom(PlayerController player)
        {
            if (!IsHitable)
                return;

            _boxAnimator.SetTrigger("Jump");
            IsHitable = false;
        }
        public virtual void OnJumpCompleted()
        {
        }
        protected void InstantiateContent()
        {
            var content = Instantiate(_contentPrefab);
            content.transform.position = this.transform.position;
        }
    }
}