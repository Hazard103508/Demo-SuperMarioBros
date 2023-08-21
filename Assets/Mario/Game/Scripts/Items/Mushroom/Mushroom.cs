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
        #endregion

        #region Properties
        public MushroomStateMachine StateMachine { get; protected set; }
        public Movable Movable { get; private set; }
        public MushroomProfile Profile => _profile;
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new MushroomStateMachine(this);
            Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateRising);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        private void OnEnable()
        {
            StartCoroutine(ResetMushroom());
        }
        #endregion

        #region Public Methods
        public void OnFall() => gameObject.SetActive(false);
        public void ChangeDirectionToRight() => Movable.Speed = Mathf.Abs(Movable.Speed);
        public void ChangeDirectionToLeft() => Movable.Speed = -Mathf.Abs(Movable.Speed);
        public void ChangeSpeedAfferHit(Vector3 hitPosition)
        {
            if (Math.Sign(Movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
                Movable.Speed *= -1;
        }
        #endregion

        #region Private Methods
        private IEnumerator ResetMushroom()
        {
            yield return new WaitForEndOfFrame();
            this.StateMachine.TransitionTo(this.StateMachine.StateRising);
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToBottom(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToBottom(hitInfo);
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
    }
}