using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using Mario.Game.ScriptableObjects.Pool;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes.Box
{
    public class Box : MonoBehaviour,
        IHittableByMovingToTop,
        IHittableByPlayerFromBottom
    {
        #region Objects
        [SerializeField] private AudioSource _hitSoundFX;
        //private Animator _boxAnimator;
        //private Movable _movable;
        //private float _yPosition;
        #endregion

        //#region Objects
        [SerializeField] private BoxProfile _profile;
        [SerializeField] private Animator _animator;
        //[SerializeField] private SpriteRenderer _renderer;
        //[SerializeField] private AudioSource _hitSoundFX;
        //[SerializeField] private AudioSource _kickSoundFX;
        //#endregion

        #region Properties
        public BoxStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public BoxProfile Profile => _profile;
        #endregion

        #region Properties
        public bool IsLastJump { get; protected set; }
        protected bool IsHitable { get; set; }
        public bool IsJumping { get; private set; }
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new BoxStateMachine(this);
            this.Movable = GetComponent<Movable>();
            this.IsLastJump = true;
            //IsHitable = true;
            //_yPosition = transform.position.y;
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateIdle);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        #endregion

        #region Protected Methods
        protected virtual void OnJumpCompleted()
        {
            IsJumping = false;
        }
        protected void ShowContent(PooledObjectProfile profile) => Services.PoolService.GetObjectFromPool(profile, this.transform.position);
        protected void PlayHitSoundFX()
        {
            if (_hitSoundFX.enabled && !_hitSoundFX.isPlaying)
                _hitSoundFX.Play();
        }
        #endregion

        #region Private Methods
        private void HitObjectOnTop(RayHitInfo hitInfo)
        {
            if (!IsJumping)
                return;

            if (hitInfo.hitObjects.Any())
            {
                foreach (var obj in hitInfo.hitObjects)
                {
                    var hitableObject = obj.Object.GetComponent<IHittableByBox>();
                    hitableObject?.OnHittedByBox(this.gameObject);
                }
            }
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) { }
        #endregion

        #region On Player Hit
        public virtual void OnHittedByPlayerFromBottom(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        //{
        //    if (!IsHitable)
        //        return;
        //
        //    IsJumping = true;
        //    //_boxAnimator.SetTrigger("Jump");
        //    IsHitable = false;
        //}
        #endregion
    }
}