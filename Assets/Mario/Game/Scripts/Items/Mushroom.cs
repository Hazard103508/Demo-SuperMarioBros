using UnityEngine;
using static Mario.Game.Player.PlayerController;
using UnityEngine.Profiling;
using Mario.Game.ScriptableObjects;
using Mario.Game.Enums;
using UnityShared.Commons.Structs;
using UnityShared.Behaviours.Various.RaycastRange;
using System;

namespace Mario.Game.Items
{
    public class Mushroom : MonoBehaviour
    {
        [SerializeField] private MushroomProfile _mushroomProfile;
        [SerializeField] private RaycastRange[] raycastRanges = null;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityHit = new Bounds<bool>();

        private void Awake()
        {
            _currentSpeed = Vector2.right * _mushroomProfile.MoveSpeed;
            Array.ForEach(raycastRanges, r => r.SpriteSize = new Size2(1, 1));
        }
        private void Update()
        {
            //GatherInput();

            CalculateWalk();
            //CalculateGravity();
            Move();
        }

        private void CalculateWalk()
        {
            if (_currentSpeed.x > 0 && _proximityHit.right || _proximityHit.left)
                _currentSpeed.x *= -1;
        }
        private void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;

            /*

            // ajusto posicion de contacto con el suelo
            if (IsGrounded && RawMovement.y == 0)
            {
                if (this.Mode == PlayerModes.Small)
                {
                    float fixPos = (int)nextPosition.y + _controllerVariables.smallAdjustmentPositionY;
                    float dif = fixPos - nextPosition.y;
                    nextPosition.y = nextPosition.y + dif; // ajusto diferencia en posicion del personaje
                }
                else
                    nextPosition.y = Mathf.Round(nextPosition.y);
            }

            // fuerzo ajuste de posicion en los lados de los bloques 
            if (!_controllerVariables.ProximityHit.bottom)
            {
                if (!_controllerVariables.ProximityHit.left && _controllerVariables.ProximityHit.right)
                    nextPosition.x -= _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
                if (!_controllerVariables.ProximityHit.right && _controllerVariables.ProximityHit.left)
                    nextPosition.x += _profile.Jump.HorizontalAdjustmentSpeed * Time.deltaTime;
            }
            */
            transform.position = nextPosition;
        }

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityHit.left = hitInfo.IsHiting;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityHit.right = hitInfo.IsHiting;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityHit.bottom = hitInfo.IsHiting;
        #endregion
    }
}