using UnityEngine;
using UnityEngine.InputSystem;

namespace Mario.Game.Player
{
    public class PlayerInputActions : MonoBehaviour
    {
        #region Properties
        public Vector2 Move { get; private set; }
        public bool Jump { get; private set; }
        public bool Sprint { get; private set; }
        #endregion

        #region Input System Methods
        public void OnMove(InputValue value) => Move = value.Get<Vector2>();
        public void OnJump(InputValue value) => Jump = value.isPressed;
        public void OnSprint(InputValue value) => Sprint = value.isPressed;
        #endregion
    }
}