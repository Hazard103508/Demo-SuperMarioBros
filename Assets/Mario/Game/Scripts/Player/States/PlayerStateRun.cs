using Mario.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateRun : PlayerState
    {
        #region Constructor
        public PlayerStateRun(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnFall() => SetTransitionToDeathFall();
        public override void OnDeath() => SetTransitionToDeath();
        #endregion

        #region Protected Methods
        protected override string GetAnimatorState() => "Run";
        #endregion

        #region IState Methods
        public override void Update()
        {
            SpeedUp();
            SpeedDown();
            SetAnimationSpeed();

            if (SetTransitionToJump())
                return;

            if (SetTransitionToFall())
                return;

            if (SetTransitionToStop())
                return;

            if (SetTransitionToIdle())
                return;

            SetSpriteDirection();
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