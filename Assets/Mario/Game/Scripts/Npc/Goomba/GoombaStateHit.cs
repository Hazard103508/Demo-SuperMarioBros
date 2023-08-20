using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateHit : GoombaState
    {
        #region Objects
        private float _timer = 0;
        #endregion

        #region Constructor
        public GoombaStateHit(Goomba goomba) : base(goomba)
        {
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Goomba.Movable.enabled = false;
            Goomba.gameObject.layer = 0;
            Goomba.Animator.SetTrigger("Hit");
            Goomba.PlayHitSoundFX();

            Services.ScoreService.Add(Goomba.Profile.Points);
            Services.ScoreService.ShowPoints(Goomba.Profile.Points, Goomba.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.4f)
                GameObject.Destroy(Goomba.gameObject);
        }
        #endregion
    }
}