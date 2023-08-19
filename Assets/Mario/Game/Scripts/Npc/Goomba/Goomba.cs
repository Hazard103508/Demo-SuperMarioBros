using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Goomba
{
    public class Goomba : MonoBehaviour,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {
        #region Objects
        [SerializeField] private GoombaProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private Animator _animator;
        #endregion


        #region Properties
        public GoombaStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public GoombaProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            this.StateMachine = new GoombaStateMachine(this);
            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;

            Movable = GetComponent<Movable>();
            Movable.Gravity = _profile.FallSpeed;
            Movable.MaxFallSpeed = _profile.MaxFallSpeed;
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateWalk);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        private void OnDestroy()
        {
            Services.PlayerService.CanMoveChanged -= OnCanMoveChanged;
        }
        #endregion

        #region Public Methods
        public void OnFall() => Destroy(gameObject);
        public void ChangeDirectionToRight()
        {
            _renderer.flipX = true;
            Movable.Speed = Mathf.Abs(Movable.Speed);
        }
        public void ChangeDirectionToLeft()
        {
            _renderer.flipX = false;
            Movable.Speed = -Mathf.Abs(Movable.Speed);
        }
        public void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
                Movable.Speed *= -1;
        }
        public void PlayHitSoundFX() => _hitSoundFX.Play();
        public void PlayKickSoundFX() => _kickSoundFX.Play();
        #endregion

        #region Private Methods
        //private void Kill(Vector3 hitPosition)
        //{
        //    _movable.ChekCollisions = false;
        //    gameObject.layer = 0;
        //
        //    _kickSoundFX.Play();
        //    _animator.SetTrigger("Kill");
        //    _renderer.sortingLayerName = "Dead";
        //
        //    Services.ScoreService.Add(_profile.Points);
        //    Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.8f, 3f);
        //
        //    if (Math.Sign(_movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
        //        _movable.Speed *= -1;
        //
        //    _movable.AddJumpForce(_profile.JumpAcceleration);
        //}
        //private void Hit(PlayerController player)
        //{
        //    gameObject.layer = 0; // Deshabilitado para otra colision
        //    _movable.enabled = false;
        //
        //    _hitSoundFX.Play();
        //    enabled = false;
        //    _animator.SetTrigger("Hit");
        //
        //    Services.ScoreService.Add(_profile.Points);
        //    Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        //
        //    player.BounceJump();
        //    StartCoroutine(DestroyAfterHit());
        //}
        //private IEnumerator DestroyAfterHit()
        //{
        //    yield return new WaitForSeconds(0.4f);
        //    Destroy(gameObject);
        //}
        //private void DamagePlayer(PlayerController player)
        //{
        //    player.DamagePlayer();
        //}
        //private void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        //private void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => this.StateMachine.CurrentState.OnHittedByBox(box);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa.Koopa koopa) => this.StateMachine.CurrentState.OnHittedByKoppa(koopa);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => this.StateMachine.CurrentState.OnHittedByFireBall(fireball);
        #endregion
    }
}