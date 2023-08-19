using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateDead : GoombaState
    {
        #region Objects
        private readonly Goomba _goomba;
        #endregion

        #region Constructor
        public GoombaStateDead(Goomba goomba)
        {
            _goomba = goomba;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _goomba.Movable.ChekCollisions = false;
            _goomba.Movable.enabled = true;
            _goomba.gameObject.layer = 0;
            _goomba.Animator.SetTrigger("Kill");
            _goomba.Renderer.sortingLayerName = "Dead";
            _goomba.PlayKickSoundFX();
            
            Services.ScoreService.Add(_goomba.Profile.Points);
            Services.ScoreService.ShowPoints(_goomba.Profile.Points, _goomba.transform.position + Vector3.up * 2f, 0.8f, 3f);
            _goomba.Movable.AddJumpForce(_goomba.Profile.JumpAcceleration);
            _goomba.Renderer.transform.position += Vector3.up * 0.5f;
        }
        #endregion
    }
}