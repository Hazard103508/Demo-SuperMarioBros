using Mario.Game.Enums;
using UnityEngine;

namespace Mario.Game.Player
{
    public abstract class PlayerAnimationMode
    {
        protected abstract int GetHash(PlayerAnimationStates currentState, PlayerAnimationFrames playerAnimationFrames);
        public void ChangeAnimation(Animator animator, PlayerAnimationStates currentState, PlayerAnimationFrames playerAnimationFrames)
        {
            int hashId = this.GetHash(currentState, playerAnimationFrames);
            animator.CrossFade(hashId, 0, 0);
            animator.speed = 1;
        }

    }
    public class PlayerAnimationModeSmall : PlayerAnimationMode
    {
        protected override int GetHash(PlayerAnimationStates currentState, PlayerAnimationFrames playerAnimationFrames)
        {
            return currentState switch
            {
                PlayerAnimationStates.Idle => Animator.StringToHash("Small_Idle"),
                PlayerAnimationStates.Jumping => Animator.StringToHash("Small_Jump"),
                PlayerAnimationStates.StoppingRun => Animator.StringToHash("Small_Stop"),
                PlayerAnimationStates.Running => Animator.StringToHash("Small_Run"),
                PlayerAnimationStates.PowerUp => Animator.StringToHash("Small_GrowingUp"),
                PlayerAnimationStates.Died => Animator.StringToHash("Small_Died"),
                PlayerAnimationStates.Flag => Animator.StringToHash("Small_Flag"),
                _ => throw new System.NotImplementedException(),
            };
        }
    }
    public class PlayerAnimationModeBig : PlayerAnimationMode
    {
        protected override int GetHash(PlayerAnimationStates currentState, PlayerAnimationFrames playerAnimationFrames)
        {
            if (currentState == PlayerAnimationStates.PowerUp)
            {
                return playerAnimationFrames switch
                {
                    PlayerAnimationFrames.Idle => Animator.StringToHash("Big_Flower_Idle"),
                    PlayerAnimationFrames.Jumping => Animator.StringToHash("Big_Flower_Jump"),
                    PlayerAnimationFrames.StoppingRun => Animator.StringToHash("Big_Flower_Stop"),
                    PlayerAnimationFrames.Ducking => Animator.StringToHash("Big_Flower_Ducking"),
                    PlayerAnimationFrames.Running1 => Animator.StringToHash("Big_Flower_Run1"),
                    PlayerAnimationFrames.Running2 => Animator.StringToHash("Big_Flower_Run2"),
                    PlayerAnimationFrames.Running3 => Animator.StringToHash("Big_Flower_Run3"),
                    _ => throw new System.NotImplementedException(),
                };
            }
            else
                return currentState switch
                {
                    PlayerAnimationStates.Idle => Animator.StringToHash("Big_Idle"),
                    PlayerAnimationStates.Jumping => Animator.StringToHash("Big_Jump"),
                    PlayerAnimationStates.StoppingRun => Animator.StringToHash("Big_Stop"),
                    PlayerAnimationStates.Running => Animator.StringToHash("Big_Run"),
                    PlayerAnimationStates.Ducking => Animator.StringToHash("Big_Ducking"),
                    PlayerAnimationStates.PowerUp => Animator.StringToHash("Big_Flower_Idle"),
                    PlayerAnimationStates.Died => Animator.StringToHash("Small_Died"),
                    PlayerAnimationStates.Flag => Animator.StringToHash("Big_Flag"),
                    PlayerAnimationStates.PowerDown => Animator.StringToHash("Big_PowerDown"),
                    _ => throw new System.NotImplementedException(),
                };
        }
    }
    public class PlayerAnimationModeSuper : PlayerAnimationMode
    {
        protected override int GetHash(PlayerAnimationStates currentState, PlayerAnimationFrames playerAnimationFrames)
        {
            return currentState switch
            {
                PlayerAnimationStates.Idle => Animator.StringToHash("Super_Idle"),
                PlayerAnimationStates.Jumping => Animator.StringToHash("Super_Jump"),
                PlayerAnimationStates.StoppingRun => Animator.StringToHash("Super_Stop"),
                PlayerAnimationStates.Running => Animator.StringToHash("Super_Run"),
                PlayerAnimationStates.Ducking => Animator.StringToHash("Super_Ducking"),
                PlayerAnimationStates.Died => Animator.StringToHash("Super_Died"),
                PlayerAnimationStates.Flag => Animator.StringToHash("Super_Flag"),
                PlayerAnimationStates.PowerDown => Animator.StringToHash("Big_PowerDown"),
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}