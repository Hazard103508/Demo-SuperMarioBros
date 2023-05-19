using Mario.Game.Enums;
using System.Threading;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace Mario.Game.Player
{
    public abstract class PlayerAnimationMode
    {
        public abstract int GetHash(PlayerAnimationStates currentState, PlayerAnimationStates previousState);

    }
    public class PlayerAnimationModeSmall : PlayerAnimationMode
    {
        public override int GetHash(PlayerAnimationStates currentState, PlayerAnimationStates previousState)
        {
            return currentState switch
            {
                PlayerAnimationStates.Idle => Animator.StringToHash("Small_Idle"),
                PlayerAnimationStates.Jumping => Animator.StringToHash("Small_Jump"),
                PlayerAnimationStates.StoppingRun => Animator.StringToHash("Small_Stop"),
                PlayerAnimationStates.Running => Animator.StringToHash("Small_Run"),
                PlayerAnimationStates.PowerUp => Animator.StringToHash("Small_GrowingUp"),
            };
        }
    }
    public class PlayerAnimationModeBig : PlayerAnimationMode
    {
        public override int GetHash(PlayerAnimationStates currentState, PlayerAnimationStates previousState)
        {
            if (currentState == PlayerAnimationStates.PowerUp)
            {
                return previousState switch
                {
                    PlayerAnimationStates.Idle => Animator.StringToHash("Big_Flower_Idle"),
                    PlayerAnimationStates.Jumping => Animator.StringToHash("Big_Flower_Jump"),
                    PlayerAnimationStates.StoppingRun => Animator.StringToHash("Big_Flower_Stop"),
                    //PlayerAnimationStates.Running => Animator.StringToHash("Super_Run"),
                    PlayerAnimationStates.Ducking => Animator.StringToHash("Big_Flower_Ducking"),
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
                };
        }
    }
    public class PlayerAnimationModeSuper : PlayerAnimationMode
    {
        public override int GetHash(PlayerAnimationStates currentState, PlayerAnimationStates previousState)
        {
            return currentState switch
            {
                PlayerAnimationStates.Idle => Animator.StringToHash("Super_Idle"),
                PlayerAnimationStates.Jumping => Animator.StringToHash("Super_Jump"),
                PlayerAnimationStates.StoppingRun => Animator.StringToHash("Super_Stop"),
                PlayerAnimationStates.Running => Animator.StringToHash("Super_Run"),
                PlayerAnimationStates.Ducking => Animator.StringToHash("Super_Ducking"),
            };
        }
    }
}