using UnityShared.Behaviours.InputActions;
using UnityShared.Behaviours.PlayerAnimators;

namespace UnityShared.Behaviours.Controllers.Players
{
    public class ThirdPersonPlayerWarriorController : ThirdPersonPlayerController
    {
        private ThirdPersonPlayerWarriorInputActions PlayerActions { get => (ThirdPersonPlayerWarriorInputActions)base.playerActions; set => base.playerActions = value; }
        private PlayerWarriorAnimator WarriorAnimator { get => (PlayerWarriorAnimator)base.playerAnimator; set => base.playerAnimator = value; }


        protected override void Update()
        {
            base.Update();

            if (PlayerActions.attack)
            {
                WarriorAnimator.SetAttack();
                PlayerActions.attack = false;
            }
        }
    }
}