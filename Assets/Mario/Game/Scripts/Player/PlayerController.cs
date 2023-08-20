using Mario.Game.Commons;
using Mario.Game.ScriptableObjects.Player;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        #region Objects
        [SerializeField] private PlayerProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        #endregion

        #region Properties
        public PlayerStateMachine StateMachine { get; private set; }
        public Movable Movable { get; private set; }
        public Animator Animator => _animator;
        public PlayerProfile Profile => _profile;
        public SpriteRenderer Renderer => _renderer;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            this.StateMachine = new PlayerStateMachine(this);
            Movable = GetComponent<Movable>();
        }
        private void Start()
        {
            this.StateMachine.Initialize(this.StateMachine.StateSmallIdle);
        }
        private void Update()
        {
            this.StateMachine.Update();
        }
        #endregion
    }
}