using UnityEngine;

namespace Mario.Game.Player
{
    public abstract class PlayerAnimationMode
    {
        public int HashIdIdle { get; protected set; }
        public int HashIdJump { get; protected set; }
        public int HashIdRun { get; protected set; }
        public int HashIdStop { get; protected set; }
        public int HashIdDying { get; protected set; }
        public int HashIdPowerDown { get; protected set; }
        public int HashIdPowerUp { get; protected set; }
        public int HashIdDucking { get; protected set; }
        public int HashIdFlag { get; protected set; }
    }
    public class PlayerAnimationModeSmall : PlayerAnimationMode
    {
        public PlayerAnimationModeSmall()
        {
            HashIdIdle = Animator.StringToHash("Small_Idle");
            HashIdJump = Animator.StringToHash("Small_Jump");
            HashIdStop = Animator.StringToHash("Small_Stop");
            HashIdRun = Animator.StringToHash("Small_Run");
            HashIdPowerUp = Animator.StringToHash("Small_PowerUp");
        }
    }
    public class PlayerAnimationModeBig : PlayerAnimationMode
    {
        public PlayerAnimationModeBig()
        {
            HashIdIdle = Animator.StringToHash("Big_Idle");
            HashIdJump = Animator.StringToHash("Big_Jump");
            HashIdStop = Animator.StringToHash("Big_Stop");
            HashIdRun = Animator.StringToHash("Big_Run");
            HashIdDucking = Animator.StringToHash("Big_Ducking");
        }
    }
    public class PlayerAnimationModeSuper : PlayerAnimationMode
    {
        public PlayerAnimationModeSuper()
        {
        }
    }
}