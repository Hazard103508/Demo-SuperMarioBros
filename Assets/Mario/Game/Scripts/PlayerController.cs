using Mario.Game.ScriptableObjects;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityShared.Behaviours.Controllers.Players.TarodevController;

namespace Mario.Game
{
    public class PlayerController : MonoBehaviour
    {
        public PlayerProfile playerProfile;

        private Vector2 _currentSpeed;


        public PlayerInput Input { get; private set; }
        public Vector3 RawMovement { get; private set; }
        public float SpeedFactor => Mathf.Abs(_currentSpeed.x) / playerProfile.walk.MaxSpeed;
        public bool Grounded { get; private set; }


        private void Awake()
        {
            Input = new PlayerInput();
        }
        private void Update()
        {
            Grounded = true; // harcodeado por ahora

            GatherInput();
            CalculateWalk();

            MoveCharacter();
        }

        private void CalculateWalk()
        {
            if (Input.X != 0)
            {
                float currentAcceleration = Input.Run ? playerProfile.run.Acceleration : playerProfile.walk.Acceleration;
                _currentSpeed.x += Input.X * currentAcceleration * Time.deltaTime;

                float currentSpeed = Input.Run ? playerProfile.run.MaxSpeed : playerProfile.walk.MaxSpeed;
                _currentSpeed.x = Mathf.Clamp(_currentSpeed.x, -currentSpeed, currentSpeed);
            }
            else
            {
                float currentDeacceleration = Input.Run ? playerProfile.run.Deacceleration : playerProfile.walk.Deacceleration;
                _currentSpeed.x = Mathf.MoveTowards(_currentSpeed.x, 0, currentDeacceleration * Time.deltaTime);
            }
        }
        private void GatherInput()
        {
            Input = new PlayerInput
            {
                JumpDown = UnityEngine.Input.GetKeyDown(KeyCode.X),
                JumpUp = UnityEngine.Input.GetKeyUp(KeyCode.X),
                X = UnityEngine.Input.GetAxisRaw("Horizontal"),
                Run = UnityEngine.Input.GetKey(KeyCode.Z),
            };
        }
        private void MoveCharacter()
        {
            RawMovement = _currentSpeed; 
            transform.position += RawMovement * Time.deltaTime;
        }
    }

    public class PlayerInput
    {
        public float X { get; set; }
        public bool JumpDown { get; set; }
        public bool JumpUp { get; set; }
        public bool Run { get; set; }
    }
}