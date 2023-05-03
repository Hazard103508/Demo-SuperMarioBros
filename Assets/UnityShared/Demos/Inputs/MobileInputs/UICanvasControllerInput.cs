using UnityEngine;
using UnityShared.Behaviours.InputActions;

namespace UnityShared.Demos.Inputs
{
    public class UICanvasControllerInput : MonoBehaviour
    {

        [Header("Output")]
        public ThirdPersonPlayerInputActions inputsActions;

        public void VirtualMoveInput(Vector2 virtualMoveDirection)
        {
            inputsActions.MoveInput(virtualMoveDirection);
        }

        public void VirtualLookInput(Vector2 virtualLookDirection)
        {
            inputsActions.LookInput(virtualLookDirection);
        }

        public void VirtualJumpInput(bool virtualJumpState)
        {
            inputsActions.JumpInput(virtualJumpState);
        }

        public void VirtualSprintInput(bool virtualSprintState)
        {
            inputsActions.SprintInput(virtualSprintState);
        }

    }

}
