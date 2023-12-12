using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Commons.Structs;
using Mario.Game.Interactable;
using Mario.Game.Player;
using UnityEngine;

namespace Mario.Game.Npc.Koopa
{
    public class KoopaStateWalk : KoopaState
    {
        #region Objects
        private IGameplayService _gameplayService;
        #endregion

        #region Constructor
        public KoopaStateWalk(Koopa koopa) : base(koopa)
        {
            _gameplayService = ServiceLocator.Current.Get<IGameplayService>();
        }
        #endregion

        #region IState Methods
        public override void Enter()
        {
            Koopa.Animator.SetTrigger("Idle");

            Koopa.gameObject.layer = LayerMask.NameToLayer("NPC");

            Koopa.Renderer.flipY = false;
            Koopa.Renderer.flipX = true;
            Koopa.Renderer.transform.localPosition = new Vector3(0.5f, 0f);
            Koopa.Renderer.sortingLayerName = "NPC";

            Koopa.Movable.ChekCollisions = true;
            Koopa.Movable.enabled = true;
            Koopa.Movable.Speed = Koopa.Profile.MoveSpeed * GetDirection();
            Koopa.Movable.Gravity = Koopa.Profile.FallSpeed;
            Koopa.Movable.MaxFallSpeed = Koopa.Profile.MaxFallSpeed;
            Koopa.Movable.SetJumpForce(0);
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
                Koopa.StateMachine.TransitionTo(Koopa.StateMachine.StateInShell);
                player.BounceJump();
            }
        }
        public override void OnHittedByPlayerFromLeft(PlayerController player) => player.Hit(Koopa);
        public override void OnHittedByPlayerFromRight(PlayerController player) => player.Hit(Koopa);
        public override void OnHittedByPlayerFromBottom(PlayerController player) => player.Hit(Koopa);
        #endregion

        #region On Box Hit
        public override void OnHittedByBox(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public override void OnHittedByKoppa(Koopa koopa) => Kill(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public override void OnHittedByFireBall(Fireball fireball) => Kill(fireball.GetHitPosition());
        #endregion
    }
}