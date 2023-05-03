using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityShared.Behaviours.InputActions
{
    public class ThirdPersonPlayerInputActions : Base3DPlayerInputActions
    {
        [Header("Character Input Values")]
        public Vector2 look;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
                LookInput(value.Get<Vector2>());
        }

        public void LookInput(Vector2 newLookDirection) => look = newLookDirection;

        private void OnApplicationFocus(bool hasFocus) => SetCursorState(cursorLocked);
        private void SetCursorState(bool newState) => Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }

}