using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;

namespace Mario.Game.Boxes
{
    public class InvisibleBox1UP : Box.Box, 
        IHittableByPlayerFromTop, 
        IHittableByPlayerFromLeft, 
        IHittableByPlayerFromRight
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

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController_OLD player)
        {
            if (_disabledTimer > 0) // me aseguro que el primer contacto sea desde abajo
                return;

            //if (!IsHitable)
            //    return;

            //PlayHitSoundFX();

            gameObject.layer = LayerMask.NameToLayer("Ground");
            base.OnHittedByPlayerFromBottom(player);
            _spriteAnimator.SetTrigger("Disable");
            _risingSoundFX.Play();
        }
        public void OnHittedByPlayerFromTop(PlayerController_OLD player) => _disabledTimer = 0.5f;
        public void OnHittedByPlayerFromLeft(PlayerController_OLD player) => _disabledTimer = 0.5f;
        public void OnHittedByPlayerFromRight(PlayerController_OLD player) => _disabledTimer = 0.5f;
        #endregion
    }
}