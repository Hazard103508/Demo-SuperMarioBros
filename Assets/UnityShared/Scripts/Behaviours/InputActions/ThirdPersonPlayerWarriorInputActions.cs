using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityShared.Behaviours.InputActions
{
    public class ThirdPersonPlayerWarriorInputActions : ThirdPersonPlayerInputActions
    {
        [Header("Character Input Values")]
        public bool attack = false;

        public void OnAttack(InputValue value) => AttackInput(value.isPressed);
        public void AttackInput(bool attack) => this.attack = attack;
    }

}