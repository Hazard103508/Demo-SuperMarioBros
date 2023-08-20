using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items.Mushroom
{
    public class MushroomStateWalk : MushroomState
    {
        #region Constructor
        public MushroomStateWalk(Mushroom mushroom) :base(mushroom)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Mushroom.Movable.enabled = true;
            Mushroom.Movable.Speed = Mushroom.Profile.MoveSpeed;
            Mushroom.Movable.Gravity = Mushroom.Profile.FallSpeed;
            Mushroom.Movable.MaxFallSpeed = Mushroom.Profile.MaxFallSpeed;
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Mushroom.ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Mushroom.ChangeDirectionToLeft();
        }
        #endregion

        #region On Box Hit
        //public override void OnHittedByBox(GameObject box) => KillGoomba(box.transform.position);
        #endregion
    }
}