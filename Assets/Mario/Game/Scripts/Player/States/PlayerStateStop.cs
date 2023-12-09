using Mario.Commons.Structs;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerStateStop : PlayerState
    {
        #region Constructor
        public PlayerStateStop(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region protected Methods
        protected override string GetAnimatorState() => "Stop";
        protected override void SetSpriteDirection() => Player.Renderer.flipX = Player.Movable.Speed > 0;
        protected override bool SetTransitionToRun() => Mathf.Sign(Player.Movable.Speed) == Mathf.Sign(Player.InputActions.Move) && base.SetTransitionToRun();
        #endregion

        #region IState Methods
        public override void Update()
        {
            SpeedUp();
            SpeedDown();

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();

            if (SetTransitionToJump())
                return;

            SetTransitionToRun();
            ShootFireball();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo) => HitObjectOnTop(hitInfo.hitObjects);
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo) => HitObjectOnBottom(hitInfo.hitObjects);
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
            HitObjectOnLeft(hitInfo.hitObjects);
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
            HitObjectOnRight(hitInfo.hitObjects);
        }
        #endregion
    }
}