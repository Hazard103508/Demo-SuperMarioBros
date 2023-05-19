using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class InvisibleBox1UP : BottomHitableBox, ITopHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] protected InvisibleBox1UPProfile _invisibleBox1UPProfile;
        [SerializeField] private Animator _spriteAnimator;
        float _disabledTimer;

        private void Update()
        {
            _disabledTimer = Mathf.Max(0, _disabledTimer - Time.deltaTime);
        }
        public override void OnHitFromBottom(PlayerController player)
        {
            if (_disabledTimer > 0) // me aseguro que el primer contacto sea desde abajo
                return;

            _hitSoundFX.Play();

            if (!IsHitable)
                return;

            gameObject.layer = LayerMask.NameToLayer("Ground");
            base.OnHitFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
        }

        public void OnHitFromTop(PlayerController player) => _disabledTimer = 0.5f;
        public void OnHitFromLeft(PlayerController player) => _disabledTimer = 0.5f;
        public void OnHitFromRight(PlayerController player) => _disabledTimer = 0.5f;

        public override void OnJumpCompleted()
        {
            base.InstantiateContent(_invisibleBox1UPProfile.GreenMushroomPrefab);
        }
    }
}