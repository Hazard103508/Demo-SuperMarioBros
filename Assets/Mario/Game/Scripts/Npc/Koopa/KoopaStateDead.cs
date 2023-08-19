using Mario.Application.Services;
using Mario.Game.Player;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateDead : KoopaState
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
        public override void Enter()
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
        #endregion
    }
}