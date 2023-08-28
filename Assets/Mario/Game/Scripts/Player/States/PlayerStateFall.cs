using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    public class PlayerStateFall : PlayerState
    {
        #region Constructor
        public PlayerStateFall(PlayerController player) : base(player)
        {
        }
        #endregion

        #region Public Methods
        public override void OnBuff() => Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateBuff);
        #endregion

        #region IState Methods
        public override void Update()
        {
            SpeedUp();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToTop(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.AddJumpForce(0);
        }
        public override void OnHittedByMovingToBottom(RayHitInfo hitInfo)
        {
            if (Player.Movable.Speed == 0)
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateIdle);
            else
                Player.StateMachine.TransitionTo(Player.StateMachine.CurrentMode.StateRun);
        }
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                Player.Movable.Speed = 0;
        }
        #endregion
    }
}