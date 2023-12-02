using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Player;
using System;

namespace Mario.Game.Boxes.Box
{
    public class BoxStateDisable : BoxState
    {
        #region Objects
        private readonly ISoundService _soundService;

        private float _cooldownTime;
        #endregion

        #region Constructor
        public BoxStateDisable(Box box) : base(box)
        {
            _soundService = ServiceLocator.Current.Get<ISoundService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Box.Renderer.sortingOrder = 0;
            Box.Animator.SetTrigger("Disable");
            Box.Movable.RemoveGravity();
            Box.Movable.ChekCollisions = false;
        }
        public override void Update()
        {
            _cooldownTime = MathF.Max(_cooldownTime - UnityEngine.Time.deltaTime, 0);
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            if (_cooldownTime == 0)
            {
                _cooldownTime = 0.5f;
                _soundService.Play(Box.Profile.HitSoundFXPoolReference, Box.transform.position);
            }
        }
        #endregion
    }
}