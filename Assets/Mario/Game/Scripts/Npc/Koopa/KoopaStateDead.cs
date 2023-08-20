using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateDead : KoopaState
    {
        #region Constructor
        public KoopaStateDead(Koopa koopa) : base(koopa)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Koopa.Movable.ChekCollisions = false;
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.MoveSpeed;
            Koopa.gameObject.layer = 0;
            Koopa.Animator.SetTrigger("Kill");
            Koopa.Renderer.sortingLayerName = "Dead";
            Koopa.PlayKickSoundFX();

            Services.ScoreService.Add(Koopa.Profile.PointsKill);
            Services.ScoreService.ShowPoints(Koopa.Profile.PointsKill, Koopa.transform.position + Vector3.up * 2f, 0.8f, 3f);
            Koopa.Movable.AddJumpForce(Koopa.Profile.JumpAcceleration);
            Koopa.Renderer.transform.position += Vector3.up * 0.5f;
        }
        #endregion
    }
}