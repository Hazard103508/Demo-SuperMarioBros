using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateDead : IKoopaState
    {
        #region Objects
        private readonly Koopa _koopa;
        #endregion

        #region Constructor
        public KoopaStateDead(Koopa koopa)
        {
            _koopa = koopa;
        }
        #endregion

        #region IState Methods
        public void Enter()
        {
            _koopa.Movable.ChekCollisions = false;
            _koopa.Movable.enabled = true;
            _koopa.Movable.Speed = _koopa.Profile.MoveSpeed;
            _koopa.gameObject.layer = 0;
            _koopa.Animator.SetTrigger("Kill");
            _koopa.Renderer.sortingLayerName = "Dead";
            _koopa.PlayKickSoundFX();

            Services.ScoreService.Add(_koopa.Profile.PointsKill);
            Services.ScoreService.ShowPoints(_koopa.Profile.PointsKill, _koopa.transform.position + Vector3.up * 2f, 0.8f, 3f);
            _koopa.Movable.AddJumpForce(_koopa.Profile.JumpAcceleration);
            _koopa.Renderer.transform.position += Vector3.up * 0.5f;
        }
        public void Exit()
        {
        }
        public void Update()
        {
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) { }
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) { }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) { }
        public void OnHittedByPlayerFromLeft(PlayerController player) { }
        public void OnHittedByPlayerFromRight(PlayerController player) { }
        public void OnHittedByPlayerFromBottom(PlayerController player) { }
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) { }
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) { }
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) { }
        #endregion
    }
}