using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class Koopa : MonoBehaviour,
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
        [SerializeField] private KoopaProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private AudioSource _blockSoundFX;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public KoopaStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public KoopaProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            this.StateMachine = new KoopaStateMachine(this);
            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;
            Movable = GetComponent<Movable>();
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
        public void PlayBlockSoundFX() => _blockSoundFX.Play();
        public void HitObject(RayHitInfo hitInfo)
        {
            var removeHits = new List<HitObject>();
            foreach (var obj in hitInfo.hitObjects)
            {
                if (obj.Object.TryGetComponent<IHittableByKoppa>(out var hitableObject))
                {
                    removeHits.Add(obj);
                    hitableObject?.OnHittedByKoppa(this);
                }
            }

            removeHits.ForEach(obj => hitInfo.hitObjects.Remove(obj));
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToLeft(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToLeft(hitInfo);
        public void OnHittedByMovingToRight(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToRight(hitInfo);
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromTop(player);
        public void OnHittedByPlayerFromLeft(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromLeft(player);
        public void OnHittedByPlayerFromRight(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromRight(player);
        public void OnHittedByPlayerFromBottom(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => this.StateMachine.CurrentState.OnHittedByBox(box);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa koopa) => this.StateMachine.CurrentState.OnHittedByKoppa(koopa);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => this.StateMachine.CurrentState.OnHittedByFireBall(fireball);
        #endregion
    }
}