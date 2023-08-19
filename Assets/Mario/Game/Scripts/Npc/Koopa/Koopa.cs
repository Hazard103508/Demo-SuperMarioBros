using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc.Koopa
{
    public class Koopa : MonoBehaviour,
        IHitableByPlayerFromTop,
        IHitableByPlayerFromBottom,
        IHitableByPlayerFromLeft,
        IHitableByPlayerFromRight,
        IHitableByBox,
        IHitableByKoppa,
        IHitableByFireBall
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
        public void ChangeDirectionToRight(RayHitInfo hitInfo)
        {
                _renderer.flipX = true;
                Movable.Speed = Mathf.Abs(Movable.Speed);
        }
        public void ChangeDirectionToLeft(RayHitInfo hitInfo)
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
            if (hitInfo.hitObjects.Any())
            {
                var removeHits = new List<HitObject>();
                foreach (var obj in hitInfo.hitObjects)
                {
                    var hitableObject = obj.Object.GetComponent<IHitableByKoppa>();
                    if (hitableObject != null)
                    {
                        removeHits.Add(obj);
                        hitableObject?.OnHittedByKoppa(this);
                    }
                }

                removeHits.ForEach(obj => hitInfo.hitObjects.Remove(obj));
            }
        }
        #endregion


        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On local Ray Range Hit
        public void OnLeftCollided(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnLeftCollided(hitInfo);
        public void OnRightCollided(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnRightCollided(hitInfo);
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
        public void OnHittedByKoppa(Koopa koopa) => this.StateMachine.CurrentState.OnHittedByKoppa(koopa);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => this.StateMachine.CurrentState.OnHittedByFireBall(fireball);
        #endregion
    }
}