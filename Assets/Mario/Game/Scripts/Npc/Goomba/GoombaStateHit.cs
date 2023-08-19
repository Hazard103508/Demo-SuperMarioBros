using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateHit : GoombaState
    {
        #region Objects
        private readonly Goomba _goomba;
        private float _timer = 0;
        #endregion

        #region Constructor
        public GoombaStateHit(Goomba goomba)
        {
            _goomba = goomba;
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            _goomba.Movable.enabled = false;
            _goomba.gameObject.layer = 0;
            _goomba.Animator.SetTrigger("Hit");
            _goomba.PlayHitSoundFX();

            Services.ScoreService.Add(_goomba.Profile.Points);
            Services.ScoreService.ShowPoints(_goomba.Profile.Points, _goomba.transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        }
        public override void Update()
        {
            _timer += Time.deltaTime;
            if (_timer >= 0.4f)
                GameObject.Destroy(_goomba.gameObject);
        }
        #endregion
    }
}