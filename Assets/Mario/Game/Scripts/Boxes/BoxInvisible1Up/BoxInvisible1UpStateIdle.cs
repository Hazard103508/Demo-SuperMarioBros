using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.Boxes.Box;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Boxes.InvisibleBox1UP
{
    public class BoxInvisible1UpStateIdle : BoxStateIdle
    {
        #region Object
        private IPlayerService _playerService;
        private Collider2D _collider2D;
        #endregion

        #region Constructor
        public BoxInvisible1UpStateIdle(Box.Box box) : base(box)
        {
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
            _collider2D = Box.GetComponent<Collider2D>();
        }
        #endregion

        #region IState Methods
        public override void Update()
        {
            base.Update();
            _collider2D.enabled = _playerService.IsStateJumping();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromBottom(PlayerController player)
        {
            Box.IsLastJump = true;
            base.OnHittedByPlayerFromBottom(player);
        }
        #endregion
    }
}