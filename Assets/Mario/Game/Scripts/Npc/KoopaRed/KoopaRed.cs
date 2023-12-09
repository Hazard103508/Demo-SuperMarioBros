using Mario.Game.Commons;
using UnityEngine;

namespace Mario.Game.Npc.KoopaRed
{
    public class KoopaRed : Koopa.Koopa
    {
        #region Objects
        [SerializeField] private RaycastRange _raycastBottomLeftEdge;
        [SerializeField] private RaycastRange _raycastBottomRighttEdge;
        private float _timer = 0f;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            this.StateMachine.StateWalk = new KoopaRedStateWalk(this);
        }
        #endregion

        #region Public Methods
        public void CheckEndFloor()
        {
            _timer = Mathf.Min(_timer + Time.deltaTime, 1);
            if (_timer >= 0.5f)
            {
                if (Movable.Speed < 0)
                    CheckEndFloor(_raycastBottomLeftEdge);
                else
                    CheckEndFloor(_raycastBottomRighttEdge);
            }
        }
        #endregion

        #region Private Methods
        private void CheckEndFloor(RaycastRange raycastRange)
        {
            var hitInfo = raycastRange.CalculateCollision(raycastRange.Profile.Ray.Length);
            if (!hitInfo.IsBlock)
            {
                StateMachine.CurrentState.ChangeDirection();
                _timer = 0f;
            }
        }
        #endregion
    }
}