using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateDead : GoombaState
    {
        #region Constructor
        public GoombaStateDead(Goomba goomba) : base(goomba)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Goomba.Movable.ChekCollisions = false;
            Goomba.Movable.enabled = true;
            Goomba.gameObject.layer = 0;
            Goomba.Animator.SetTrigger("Kill");
            Goomba.Renderer.sortingLayerName = "Dead";
            Goomba.PlayKickSoundFX();
            
            Services.ScoreService.Add(Goomba.Profile.Points);
            Services.ScoreService.ShowPoints(Goomba.Profile.Points, Goomba.transform.position + Vector3.up * 2f, 0.8f, 3f);
            Goomba.Movable.AddJumpForce(Goomba.Profile.JumpAcceleration);
            Goomba.Renderer.transform.position += Vector3.up * 0.5f;
        }
        #endregion
    }
}