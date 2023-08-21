using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Boxes;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Boxes.Box
{
    public class Box : MonoBehaviour,
        IHittableByMovingToTop,
        IHittableByPlayerFromBottom
    {
        #region Objects
        [SerializeField] private BoxProfile _profile;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public BoxStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public BoxProfile Profile => _profile;
        #endregion

        #region Properties
        public bool IsLastJump { get; set; }
        #endregion

        #region Unity Methods
        protected virtual void Awake()
        {
            this.StateMachine = new BoxStateMachine(this);
            this.Movable = GetComponent<Movable>();
            this.IsLastJump = false;
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

        #region Public Methods
        public void HitObjects(RayHitInfo hitInfo)
        {
            foreach (var obj in hitInfo.hitObjects)
            {
                var hitableObject = obj.Object.GetComponent<IHittableByBox>();
                hitableObject?.OnHittedByBox(this.gameObject);
            }
        }
        #endregion

        #region On Movable Hit
        public void OnHittedByMovingToTop(RayHitInfo hitInfo) => this.StateMachine.CurrentState.OnHittedByMovingToTop(hitInfo);
        #endregion

        #region On Player Hit
        public virtual void OnHittedByPlayerFromBottom(PlayerController_OLD player) => this.StateMachine.CurrentState.OnHittedByPlayerFromBottom(player);
        #endregion
    }
}