using System.Security.Cryptography;
using UnityEngine;

namespace Mario.Game.Player
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _anim;

        private PlayerController _player;
        private PlayerStates _state;

        public PlayerStates State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;
                var hashId =
                    State == PlayerStates.Jumping ? Skin.HashIdJump :
                    State == PlayerStates.StoppingRun ? Skin.HashIdStop :
                    State == PlayerStates.Running ? Skin.HashIdRun :
                    State == PlayerStates.PowerUp ? Skin.HashIdPowerUp :
                    Skin.HashIdIdle;

                _anim.CrossFade(hashId, 0, 0);
            }
        }
        public PlayerSkin Skin { get; set; }

        void Awake()
        {
            Skin = new PlayerSkinSmall();
            State = PlayerStates.Idle;
            _player = GetComponentInParent<PlayerController>();
        }

        void Update()
        {
            if (_player == null) return;

            if (this.State != PlayerStates.Jumping)
            {
                if (_player.Input.X != 0)
                {
                    this.State = _player.RawMovement.x != 0 && Mathf.Sign(_player.RawMovement.x) != Mathf.Sign(_player.Input.X) ? PlayerStates.StoppingRun : PlayerStates.Running;
                    transform.localScale = new Vector3(_player.Input.X > 0 ? 1 : -1, 1, 1);
                }
                else
                    State = _player.RawMovement.x != 0 ? PlayerStates.Running : PlayerStates.Idle;
            }

            if (this.State == PlayerStates.Running)
                _anim.speed = Mathf.Clamp(_player.WalkSpeedFactor, 0.5f, 1.5f);

            if (_player.RawMovement.y > 0)
                this.State = PlayerStates.Jumping;

            if (_player.RawMovement.y == 0 && this.State == PlayerStates.Jumping && _player.IsGrounded)
                this.State = PlayerStates.Idle;

            // TEMPORAL...
            if (Input.GetKeyDown(KeyCode.Space))
                this.State = PlayerStates.PowerUp;
        }
    }
    public enum PlayerStates
    {
        Idle,
        Jumping,
        Running,
        StoppingRun,
        PowerUp,
    }
    public class PlayerSkin
    {
        public int HashIdIdle { get; protected set; }
        public int HashIdJump { get; protected set; }
        public int HashIdRun { get; protected set; }
        public int HashIdStop { get; protected set; }
        public int HashIdDying { get; protected set; }
        public int HashIdPowerDown { get; protected set; }
        public int HashIdPowerUp { get; protected set; }
        public int HashIdBend { get; protected set; }
        public int HashIdFlag { get; protected set; }
    }
    public class PlayerSkinSmall : PlayerSkin
    {
        public PlayerSkinSmall()
        {
            HashIdIdle = Animator.StringToHash("Small_Idle");
            HashIdJump = Animator.StringToHash("Small_Jump");
            HashIdStop = Animator.StringToHash("Small_Stop");
            HashIdRun = Animator.StringToHash("Small_Run");
            HashIdPowerUp = Animator.StringToHash("Small_PowerUp");
        }
    }
    public class PlayerSkinBig : PlayerSkin
    {
        public PlayerSkinBig()
        {
            HashIdIdle = Animator.StringToHash("Big_Idle");
            HashIdJump = Animator.StringToHash("Big_Jump");
            HashIdStop = Animator.StringToHash("Big_Stop");
            HashIdRun = Animator.StringToHash("Big_Run");
        }
    }
}