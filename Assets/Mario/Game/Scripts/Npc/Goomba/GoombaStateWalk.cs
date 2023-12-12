using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Goomba
{
    public class GoombaStateWalk : GoombaState
    {
        #region Objects
        private IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public GoombaStateWalk(Goomba goomba) : base(goomba)
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Goomba.Animator.SetTrigger("Idle");

            Goomba.gameObject.layer = LayerMask.NameToLayer("NPC");
            Goomba.Movable.ChekCollisions = true;
            Goomba.Movable.enabled = true;
            Goomba.Movable.Speed = Goomba.Profile.MoveSpeed;
            Goomba.Movable.Gravity = Goomba.Profile.FallSpeed;
            Goomba.Movable.MaxFallSpeed = Goomba.Profile.MaxFallSpeed;
            Goomba.Movable.SetJumpForce(0);

            Goomba.Renderer.flipY = false;
            Goomba.Renderer.transform.localPosition = new Vector3(0.5f, 0.5f);
            Goomba.Renderer.sortingLayerName = "NPC";
        }
        public override void Update()
        {
            base.Update();
        }
        #endregion

        #region On Movable Hit
        public override void OnHittedByMovingToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToRight();
        }
        public override void OnHittedByMovingToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
                ChangeDirectionToLeft();
        }
        #endregion

        #region On Player Hit
        public override void OnHittedByPlayerFromTop(PlayerController player)
        {
            if (_gameplayService.IsStarman)
                Kill(player.transform.position);
            else
            {
                Goomba.StateMachine.TransitionTo(Goomba.StateMachine.StateHit);
                player.BounceJump();
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit(Goomba);
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit(Goomba);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => player.Hit(Goomba);
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa.Koopa koopa) => Kill(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => Kill(fireball.GetHitPosition());
        #endregion
    }
}