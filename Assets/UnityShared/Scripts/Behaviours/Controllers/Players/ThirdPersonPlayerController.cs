using System;
using UnityEngine;
using UnityShared.Behaviours.InputActions;
using UnityShared.Helpers;

namespace UnityShared.Behaviours.Controllers.Players
{
    public class ThirdPersonPlayerController : Base3DPlayerController
    {
        public CinemachineAttributes cinemachineAttributes;

        private ThirdPersonPlayerVariables thirdPersonPlayerVariables = new ThirdPersonPlayerVariables();
        private GameObject _mainCamera;

        protected bool IsCurrentDeviceMouse => playerInput.currentControlScheme == "KeyboardMouse";
        private ThirdPersonPlayerInputActions PlayerActions { get => (ThirdPersonPlayerInputActions)base.playerActions; set => base.playerActions = value; }


        protected override void Awake()
        {
            base.Awake();

            if (_mainCamera == null)
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }

        protected override void Start()
        {
            base.Start();

            thirdPersonPlayerVariables.cinemachineTargetYaw = cinemachineAttributes.CinemachineCameraTarget.transform.rotation.eulerAngles.y;
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        protected override void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = playerActions.sprint ? playerAtributes.SprintSpeed : playerAtributes.MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (playerActions.move == Vector2.zero)
                targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;
            float speedOffset = 0.1f;
            float inputMagnitude = PlayerActions.analogMovement ? playerActions.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                thirdPersonPlayerVariables.speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * playerAtributes.SpeedChangeRate);
                thirdPersonPlayerVariables.speed = Mathf.Round(thirdPersonPlayerVariables.speed * 1000f) / 1000f; // round speed to 3 decimal places
            }
            else
                thirdPersonPlayerVariables.speed = targetSpeed;

            thirdPersonPlayerVariables.animationBlend = Mathf.Lerp(thirdPersonPlayerVariables.animationBlend, targetSpeed, Time.deltaTime * playerAtributes.SpeedChangeRate);
            if (thirdPersonPlayerVariables.animationBlend < 0.01f) thirdPersonPlayerVariables.animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(playerActions.move.x, 0.0f, playerActions.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (playerActions.move != Vector2.zero)
            {
                thirdPersonPlayerVariables.targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, thirdPersonPlayerVariables.targetRotation, ref thirdPersonPlayerVariables.rotationVelocity, playerAtributes.RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }

            Vector3 targetDirection = Quaternion.Euler(0.0f, thirdPersonPlayerVariables.targetRotation, 0.0f) * Vector3.forward;

            // move the player
            characterController.Move(targetDirection.normalized * (thirdPersonPlayerVariables.speed * Time.deltaTime) + new Vector3(0.0f, base.base3DPlayerVariables.verticalVelocity, 0.0f) * Time.deltaTime);

            base.playerAnimator.SetSpeed(thirdPersonPlayerVariables.animationBlend);
            base.playerAnimator.SetMotionSpeed(inputMagnitude);
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (PlayerActions.look.sqrMagnitude >= thirdPersonPlayerVariables.threshold && !cinemachineAttributes.LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                thirdPersonPlayerVariables.cinemachineTargetYaw += PlayerActions.look.x * deltaTimeMultiplier;
                thirdPersonPlayerVariables.cinemachineTargetPitch += PlayerActions.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            thirdPersonPlayerVariables.cinemachineTargetYaw = AnglesHelper.Clamp(thirdPersonPlayerVariables.cinemachineTargetYaw, float.MinValue, float.MaxValue);
            thirdPersonPlayerVariables.cinemachineTargetPitch = AnglesHelper.Clamp(thirdPersonPlayerVariables.cinemachineTargetPitch, cinemachineAttributes.BottomClamp, cinemachineAttributes.TopClamp);

            // Cinemachine will follow this target
            cinemachineAttributes.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(thirdPersonPlayerVariables.cinemachineTargetPitch + cinemachineAttributes.CameraAngleOverride, thirdPersonPlayerVariables.cinemachineTargetYaw, 0.0f);
        }
    }

    [Serializable]
    public class CinemachineAttributes
    {
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;
    }

    public class ThirdPersonPlayerVariables
    {
        //Player
        public float speed;
        public float animationBlend;
        public float targetRotation = 0.0f;
        public float rotationVelocity;

        //Cinemachine
        public float threshold = 0.01f;
        public float cinemachineTargetYaw;
        public float cinemachineTargetPitch;
    }
}