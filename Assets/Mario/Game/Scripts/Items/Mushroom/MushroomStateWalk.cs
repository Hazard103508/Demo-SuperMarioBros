using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateWalk : MushroomState
    {
        #region Constructor
        public MushroomStateWalk(Mushroom mushroom) : base(mushroom)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Mushroom.Movable.enabled = true;
            Mushroom.Movable.Speed = Mushroom.Profile.MoveSpeed * Mathf.Sign(Mushroom.Movable.Speed);
            Mushroom.Movable.Gravity = Mushroom.Profile.FallSpeed;
            Mushroom.Movable.MaxFallSpeed = Mushroom.Profile.MaxFallSpeed;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToLeft();
        }
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box)
        {
            ChangeSpeedAfferHit(box.transform.position);
            Mushroom.StateMachine.TransitionTo(Mushroom.StateMachine.StateJump);
        }
        #endregion
    }
}