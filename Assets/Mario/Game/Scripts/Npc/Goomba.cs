using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc
{
    public class Goomba : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] private GoombaProfile _goombaProfile;
        [SerializeField] private Animator _animator;
        [SerializeField] private RaycastRange[] _raycastRanges = null;
        [SerializeField] private AudioSource _killSoundFX;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityBlock = new();

        #region Unity Methods
        private void Awake()
        {
            _currentSpeed = Vector2.right * _goombaProfile.MoveSpeed;
            Array.ForEach(_raycastRanges, r => r.SpriteSize = new Size2(0.9f, 1));
        }
        private void Update()
        {
            if (!AllServices.PlayerService.CanMove)
                return;

            CalculateWalk();
            CalculateGravity();

            Move();
        }
        #endregion

        #region Private Methods
        private void CalculateWalk()
        {
            if (_proximityBlock.right)
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            else if (_proximityBlock.left)
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
        }
        private void CalculateGravity()
        {
            _currentSpeed.y -= _goombaProfile.FallSpeed * Time.deltaTime;
            if (_proximityBlock.bottom)
            {
                if (_currentSpeed.y < 0)
                    _currentSpeed.y = 0;
            }
            else
            {
                if (_currentSpeed.y < -_goombaProfile.MaxFallSpeed)
                    _currentSpeed.y = -_goombaProfile.MaxFallSpeed;
            }
        }
        private void Move()
        {
            var nextPosition = transform.position + _currentSpeed * Time.deltaTime;

            // ajusto posicion de contacto con el suelo
            if (_proximityBlock.bottom && _currentSpeed.y == 0)
                nextPosition.y = Mathf.Round(nextPosition.y);

            transform.position = nextPosition;
        }
        private void KillGoomba(PlayerController player)
        {
            if (!enabled)
                return;

            _killSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Kill");

            AllServices.ScoreService.Add(_goombaProfile.Points);
            AllServices.ScoreService.ShowPoint(_goombaProfile.Points, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            StartCoroutine(DestroyGoomba());

            player.BounceJump();
        }
        private void DamagePlayer(PlayerController player)
        {
            if (!enabled)
                return;

            _animator.speed = 0;
            enabled = false;
            player.Kill();
        }
        private IEnumerator DestroyGoomba()
        {
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
        }
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityBlock.bottom = hitInfo.IsBlock;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo.IsBlock;
        #endregion

        #region On Player Hit
        public void OnHitFromTop(PlayerController player) => KillGoomba(player);
        public void OnHitFromBottom(PlayerController player) => DamagePlayer(player);
        public void OnHitFromLeft(PlayerController player) => DamagePlayer(player);
        public void OnHitFromRight(PlayerController player) => DamagePlayer(player);
        #endregion
    }
}