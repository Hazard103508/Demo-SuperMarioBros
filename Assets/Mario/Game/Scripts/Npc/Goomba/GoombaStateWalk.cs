using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateWalk : GoombaState
    {
        #region Objects
        private readonly Goomba _goomba;
        #endregion

        #region Constructor
        public GoombaStateWalk(Goomba goomba)
        {
            _goomba = goomba;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _goomba.Movable.enabled = true;
            _goomba.Movable.Speed = _goomba.Profile.MoveSpeed;
            _goomba.Animator.SetTrigger("Idle");
        }
        #endregion

        #region Private Methods
        private void KillGoomba(Vector3 hitPosition)
        {
            _goomba.StateMachine.TransitionTo(_goomba.StateMachine.StateDead);
            _goomba.ChangeSpeedAfferHit(hitPosition);
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                _goomba.ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                _goomba.ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            //_goomba.StateMachine.TransitionTo(_goomba.StateMachine.StateInShell);
            player.BounceJump();
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.DamagePlayer();
        public override void OnHittedByPlayerFromBottom(PlayerController player) => player.DamagePlayer();
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => KillGoomba(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa.Koopa koopa) => KillGoomba(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => KillGoomba(fireball.transform.position);
        #endregion
    }
}