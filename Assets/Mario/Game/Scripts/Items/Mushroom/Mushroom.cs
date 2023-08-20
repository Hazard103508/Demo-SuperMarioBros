using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Items.Mushroom
{
    public class Mushroom : MonoBehaviour,
        IHittableByMovingToBottom,
        IHittableByMovingToLeft,
        IHittableByMovingToRight,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox
    {
        #region Objects
        [SerializeField] private MushroomProfile _profile;
        private bool _isJumping;
        #endregion

        #region Properties
        protected bool IsRising { get; private set; }
        #endregion

        #region Properties
        public MushroomStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public MushroomProfile Profile => _profile;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            this.StateMachine = new MushroomStateMachine(this);
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
        private void OnEnable()
        {
            //ResetMushroom();
            //StartCoroutine(RiseMushroom());
        }
        #endregion

        #region Public Methods
        public void OnFall() => gameObject.SetActive(false);
        public void ChangeDirectionToRight() => Movable.Speed = Mathf.Abs(Movable.Speed);
        public void ChangeDirectionToLeft() => Movable.Speed = -Mathf.Abs(Movable.Speed);
        #endregion

        #region Protected Methods
        protected virtual void CollectMushroom(PlayerController player)
        {
        }
        protected virtual void ResetMushroom() => IsRising = false;
        #endregion

        #region Private Methods
        //private IEnumerator RiseMushroom()
        //{
        //    yield return new WaitForEndOfFrame();
        //
        //    IsRising = true;
        //    var _initPosition = transform.transform.position;
        //    var _targetPosition = _initPosition + Vector3.up;
        //    float _timer = 0;
        //    float _maxTime = 0.8f;
        //    while (_timer < _maxTime)
        //    {
        //        _timer += Time.deltaTime;
        //        var t = Mathf.InverseLerp(0, _maxTime, _timer);
        //        transform.localPosition = Vector3.Lerp(_initPosition, _targetPosition, t);
        //        yield return null;
        //    }
        //
        //    _movable.enabled = true;
        //    IsRising = false;
        //}
        #endregion

        //#region On local Ray Range Hit
        //public void OnBottomCollided(RayHitInfo hitInfo) => _isJumping = false;
        //public void OnLeftCollided(RayHitInfo hitInfo) => ChangeDirectionToRight();
        //public void OnRightCollided(RayHitInfo hitInfo) => ChangeDirectionToLeft();
        //#endregion


        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
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
        public void OnHittedByBox(GameObject box)
        {
            //if (_isJumping)
            //    return;
            //
            //_isJumping = true;
            //_movable.AddJumpForce(_profile.JumpAcceleration);
            //
            //if (Math.Sign(_movable.Speed) != Math.Sign(this.transform.position.x - box.transform.position.x))
            //    _movable.Speed *= -1;
        }
        #endregion
    }
}