using Mario.Application.Services;
using Mario.Game.Boxes;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Behaviours.Various.RaycastRange;
using UnityShared.Commons.Structs;
using UnityShared.Extensions.CSharp;

namespace Mario.Game.Npc
{
    public class Goomba : MonoBehaviour, ITopHitable, IBottomHitable, ILeftHitable, IRightHitable
    {
        [SerializeField] private GoombaProfile _goombaProfile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _raycastRanges;
        [SerializeField] private AudioSource _killSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;

        private Vector3 _currentSpeed;
        private Bounds<bool> _proximityBlock = new();
        private bool _isKicked;

        #region Unity Methods
        private void Awake()
        {
            AllServices.PlayerService.OnCanMoveChanged.AddListener(OnCanMoveChanged);
            _currentSpeed = Vector2.right * _goombaProfile.MoveSpeed;
        }
        private void OnDestroy()
        {
            AllServices.PlayerService.OnCanMoveChanged.RemoveListener(OnCanMoveChanged);
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
        private void Kill(PlayerController player)
        {
            if (!enabled || _isKicked)
                return;

            _killSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Kill");

            AllServices.ScoreService.Add(_goombaProfile.Points);
            AllServices.ScoreService.ShowPoint(_goombaProfile.Points, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            StartCoroutine(DestroyGoomba());

            player.BounceJump();
        }
        private void Kick(GameObject box)
        {
            _isKicked = true;

            _kickSoundFX.Play();
            _animator.SetTrigger("Kicked");
            _renderer.sortingLayerName = "Dead";

            AllServices.ScoreService.Add(_goombaProfile.Points);
            AllServices.ScoreService.ShowPoint(_goombaProfile.Points, transform.position + Vector3.up * 1.5f, 0.8f, 3f);

            if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - box.transform.position.x))
                _currentSpeed.x *= -1;
            _currentSpeed.y = _goombaProfile.JumpAcceleration * Time.deltaTime;

            _proximityBlock.bottom = false; // evito que colicione contra el suelo
            _proximityBlock.left = false;
            _proximityBlock.right = false;

            Destroy(_raycastRanges.gameObject);
        }
        private void DamagePlayer(PlayerController player)
        {
            if (!enabled || _isKicked)
                return;

            player.DamagePlayer();
        }
        private IEnumerator DestroyGoomba()
        {
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
        }
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = AllServices.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region External Events
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityBlock.left = hitInfo.IsBlock;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityBlock.right = hitInfo.IsBlock;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo)
        {
            _proximityBlock.bottom = hitInfo.IsBlock;
            foreach (var obj in hitInfo.hitObjects)
            {
                var box = obj.GetComponent<BottomHitableBox>();
                if (box != null && box.IsJumping)
                {
                    Kick(obj);
                    return;
                }
            };
        }
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityBlock.top = hitInfo.IsBlock;
        #endregion

        #region On Player Hit
        public void OnHitFromTop(PlayerController player) => Kill(player);
        public void OnHitFromBottom(PlayerController player) => DamagePlayer(player);
        public void OnHitFromLeft(PlayerController player) => DamagePlayer(player);
        public void OnHitFromRight(PlayerController player) => DamagePlayer(player);
        #endregion
    }
}