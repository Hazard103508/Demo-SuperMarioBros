using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityShared.Behaviours.InputActions;
using UnityShared.Behaviours.PlayerAnimators;
using UnityShared.Helpers;

namespace UnityShared.Behaviours.Controllers.Players
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(PlayerInput))]
    public class Base3DPlayerController : MonoBehaviour
    {
        public PlayerAtributes playerAtributes;
        public GroundAtributes groundAtributes;
        protected Base3DPlayerVariables base3DPlayerVariables = new();

        protected Base3DPlayerInputActions playerActions;
        protected Base3DPlayerAnimator playerAnimator;

        protected CharacterController characterController;
        protected PlayerInput playerInput;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController>();
            playerInput = GetComponent<PlayerInput>();

            playerAnimator = GetComponent<Base3DPlayerAnimator>();
            playerActions = GetComponent<Base3DPlayerInputActions>();
        }

        protected virtual void Start()
        {
            // reset our timeouts on start
            base3DPlayerVariables.jumpTimeoutDelta = playerAtributes.JumpTimeout;
            base3DPlayerVariables.fallTimeoutDelta = playerAtributes.FallTimeout;
        }

        protected virtual void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
        }

        protected virtual void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new(transform.position.x, transform.position.y - groundAtributes.GroundedOffset, transform.position.z);
            groundAtributes.Grounded = Physics.CheckSphere(spherePosition, groundAtributes.GroundedRadius, groundAtributes.GroundLayers, QueryTriggerInteraction.Ignore);

            playerAnimator.SetGrounded(groundAtributes.Grounded);
        }

        protected virtual void Move()
        {
        }

        protected virtual void JumpAndGravity()
        {
            if (groundAtributes.Grounded)
            {
                // reset the fall timeout timer
                base3DPlayerVariables.fallTimeoutDelta = playerAtributes.FallTimeout;

                playerAnimator.SetJump(false);
                playerAnimator.SetFreeFall(false);

                // stop our velocity dropping infinitely when grounded
                if (base3DPlayerVariables.verticalVelocity < 0.0f)
                    base3DPlayerVariables.verticalVelocity = -2f;

                // Jump
                if (playerActions.jump && base3DPlayerVariables.jumpTimeoutDelta <= 0.0f)
                {
                    base3DPlayerVariables.verticalVelocity = MathEquations.Trajectory.GetVelocity(playerAtributes.JumpHeight, playerAtributes.Gravity);
                    playerAnimator.SetJump(true);
                }

                // jump timeout
                if (base3DPlayerVariables.jumpTimeoutDelta >= 0.0f)
                    base3DPlayerVariables.jumpTimeoutDelta -= Time.deltaTime;
            }
            else
            {
                // reset the jump timeout timer
                base3DPlayerVariables.jumpTimeoutDelta = playerAtributes.JumpTimeout;

                // fall timeout
                if (base3DPlayerVariables.fallTimeoutDelta >= 0.0f)
                    base3DPlayerVariables.fallTimeoutDelta -= Time.deltaTime;
                else
                    playerAnimator.SetFreeFall(true);

                playerActions.jump = false; // if we are not grounded, do not jump
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (base3DPlayerVariables.verticalVelocity < base3DPlayerVariables.terminalVelocity)
                base3DPlayerVariables.verticalVelocity += playerAtributes.Gravity * Time.deltaTime;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            Color transparentGreen = new(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new(1.0f, 0.0f, 0.0f, 0.35f);

            if (groundAtributes.Grounded)
                Gizmos.color = transparentGreen;
            else
                Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            var drawPosition = new Vector3(transform.position.x, transform.position.y - groundAtributes.GroundedOffset, transform.position.z);
            Gizmos.DrawSphere(drawPosition, groundAtributes.GroundedRadius);
        }
    }

    [Serializable]
    public class PlayerAtributes
    {
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;
    }

    [Serializable]
    public class GroundAtributes
    {
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;
    }

    public class Base3DPlayerVariables
    {
        // player
        public float verticalVelocity;
        public float terminalVelocity = 53.0f;

        // timeout deltatime
        public float jumpTimeoutDelta;
        public float fallTimeoutDelta;
    }
}