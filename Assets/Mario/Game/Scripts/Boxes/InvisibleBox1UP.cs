using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class InvisibleBox1UP : Box, IHitableByPlayerFromTop, IHitableByPlayerFromLeft, IHitableByPlayerFromRight
    {
        #region Objects
        [SerializeField] private AudioSource _risingSoundFX;
        [SerializeField] protected InvisibleBox1UPProfile _invisibleBox1UPProfile;
        [SerializeField] private Animator _spriteAnimator;
        float _disabledTimer;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
        }
        private void Update()
        {
            _disabledTimer = Mathf.Max(0, _disabledTimer - Time.deltaTime);
        }
        #endregion

        #region Protected Methods
        protected override void OnJumpCompleted()
        {
            base.OnJumpCompleted();
            base.ShowContent(_invisibleBox1UPProfile.GreenMushroomPoolReference);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (_disabledTimer > 0) // me aseguro que el primer contacto sea desde abajo
                return;

            if (!IsHitable)
                return;

            PlayHitSoundFX();

            gameObject.layer = LayerMask.NameToLayer("Ground");
            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();
        }
        public void OnHittedByPlayerFromTop(PlayerController player) => _disabledTimer = 0.5f;
        public void OnHittedByPlayerFromLeft(PlayerController player) => _disabledTimer = 0.5f;
        public void OnHittedByPlayerFromRight(PlayerController player) => _disabledTimer = 0.5f;
        #endregion
    }
}