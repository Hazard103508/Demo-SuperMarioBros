using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
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

        private Coroutine _wakingUpCO;
        private bool _hitCoolDown;
        #endregion

        #region Properties
        //private KoopaStates State { get; set; }
        public KoopaStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public KoopaProfile Profile => _profile;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            //State = KoopaStates.Idle;
            this.StateMachine = new KoopaStateMachine(this);

            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;

            Movable = GetComponent<Movable>();
            Movable.Gravity = _profile.FallSpeed;
            Movable.MaxFallSpeed = _profile.MaxFallSpeed;

            //SetSpeed();
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
        public void PlayBlockSoundFX(RayHitInfo hitInfo)
        {
            //if (hitInfo.IsBlock && this.State == KoopaStates.Bouncing)
            //    _blockSoundFX.Play();
        }
        public void ChangeDirectionToRight(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
            {
                _renderer.flipX = true;
                Movable.Speed = Mathf.Abs(Movable.Speed);
            }
        }
        public void ChangeDirectionToLeft(RayHitInfo hitInfo)
        {
            if (hitInfo.IsBlock)
            {
                _renderer.flipX = false;
                Movable.Speed = -Mathf.Abs(Movable.Speed);
            }
        }
        public void Kill(Vector3 hitPosition)
        {
            Movable.ChekCollisions = false;
            Movable.enabled = true;
            gameObject.layer = 0;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            Services.ScoreService.Add(_profile.PointsHit1);
            Services.ScoreService.ShowPoints(_profile.PointsHit1, transform.position + Vector3.up * 2f, 0.8f, 3f);

            if (Math.Sign(Movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
                Movable.Speed *= -1;

            Movable.AddJumpForce(_profile.JumpAcceleration);

            if (_wakingUpCO != null)
                StopCoroutine(_wakingUpCO);

            //State = KoopaStates.Idle;
            _renderer.transform.position += Vector3.up * 0.5f;
            //SetSpeed();
        }
        public void PlayHitSoundFX() => _hitSoundFX.Play();
        //public void FirstHit(PlayerController player)
        //{
        //    if (_hitCoolDown)
        //        return;
        //
        //    _hitSoundFX.Play();
        //    _animator.SetTrigger("Hit");
        //
        //    Services.ScoreService.Add(_profile.PointsHit1);
        //    Services.ScoreService.ShowPoints(_profile.PointsHit1, transform.position + Vector3.up * 2f, 0.5f, 1.5f);
        //
        //    //State = KoopaStates.InShell;
        //    Movable.enabled = false;
        //
        //    //SetSpeed();
        //    _wakingUpCO = StartCoroutine(WakingUP());
        //
        //    player.BounceJump();
        //    StartCoroutine(Cooldown());
        //}
        #endregion

        #region Private Methods
        private void HitFromTop(PlayerController player)
        {
            //if (_hitCoolDown)
            //    return;
            //
            //if (State == KoopaStates.InShell)
            //    SecondHit(player);
            //else
            //    FirstHit(player);
        }
        private void HitFromSide(PlayerController player)
        {
            //if (_hitCoolDown)
            //    return;
            //
            //if (State == KoopaStates.InShell)
            //    SecondHit(player);
            //else
            //    DamagePlayer(player);
        }
        private void SecondHit(PlayerController player)
        {
            //if (State == KoopaStates.InShell)
            //{
            //    _animator.SetTrigger("Hit");
            //    Movable.enabled = true;
            //
            //    if (_wakingUpCO != null)
            //        StopCoroutine(_wakingUpCO);
            //
            //    _kickSoundFX.Play();
            //    Services.ScoreService.Add(_profile.PointsHit2);
            //    Services.ScoreService.ShowPoints(_profile.PointsHit2, transform.position + Vector3.up * 2f, 0.5f, 1.5f);
            //
            //    State = KoopaStates.Bouncing;
            //    SetSpeed();
            //    if (this.transform.position.x - player.transform.position.x < 0)
            //        Movable.Speed = -Mathf.Abs(Movable.Speed);
            //    else
            //        Movable.Speed = Mathf.Abs(Movable.Speed);
            //
            //    StartCoroutine(Cooldown());
            //}
        }
        //private IEnumerator WakingUP()
        //{
        //    yield return new WaitForSeconds(4f);
        //    _animator.SetTrigger("WakeUp");
        //    yield return new WaitForSeconds(1.5f);
        //    _animator.SetTrigger("Idle");
        //
        //    //State = KoopaStates.Idle;
        //    Movable.enabled = true;
        //}
        private IEnumerator Cooldown()
        {
            _hitCoolDown = true;
            yield return new WaitForSeconds(.1f);
            _hitCoolDown = false;
        }
        //private void SetSpeed()
        //{
        //    float _direction = Mathf.Sign(Movable.Speed);
        //    //Movable.Speed = (State == KoopaStates.Bouncing ? Math.Abs(_profile.BouncingSpeed) : Math.Abs(_profile.MoveSpeed)) * _direction;
        //}
        private void HitToLeft(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            PlayBlockSoundFX(hitInfo);
            //ChangeDirectionToRight(hitInfo);
        }
        private void HitToRight(RayHitInfo hitInfo)
        {
            HitObject(hitInfo);
            PlayBlockSoundFX(hitInfo);
            //ChangeDirectionToLeft(hitInfo);
        }
        private void HitObject(RayHitInfo hitInfo)
        {
            //if (State == KoopaStates.Bouncing)
            //{
            //    if (hitInfo.hitObjects.Any())
            //    {
            //        var hitObj = hitInfo.hitObjects.Select(hit => new
            //        {
            //            HitInfo = hit,
            //            HittedObject = hit.Object.GetComponent<IHitableByKoppa>()
            //        })
            //        .ToList();
            //
            //        hitInfo.IsBlock = hitInfo.IsBlock && hitObj.Any(hit => hit.HittedObject == null);
            //        hitObj.ForEach(hit => hit.HittedObject?.OnHittedByKoppa(this));
            //    }
            //}
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
        public void OnHittedByKoppa(Koopa koopa)
        {
            //if (this.State == KoopaStates.Bouncing)
            //    koopa.Kill(this.transform.position);
            //
            //this.Kill(koopa.transform.position);
        }
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => Kill(fireball.transform.position);
        #endregion

        //#region Structures
        //public enum KoopaStates
        //{
        //    Idle,
        //    InShell,
        //    Bouncing,
        //}
        //#endregion
    }
}