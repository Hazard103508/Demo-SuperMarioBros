using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityShared.Behaviours.InputActions
{
    public class Base3DPlayerInputActions : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public bool jump;
        public bool sprint;

        public void OnMove(InputValue value) => MoveInput(value.Get<Vector2>());
        public void OnJump(InputValue value) => JumpInput(value.isPressed);
        public void OnSprint(InputValue value) => SprintInput(value.isPressed);

        public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
        public void JumpInput(bool newJumpState) => jump = newJumpState;
        public void SprintInput(bool newSprintState) => sprint = newSprintState;
    }
}