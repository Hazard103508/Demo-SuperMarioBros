using Mario.Application.Services;
using Mario.Game.Commons;
using Mario.Game.Interfaces;
using Mario.Game.Npc.Koopa;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc
{
    public class Goomba : MonoBehaviour,
        IHittableByPlayerFromTop,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByBox,
        IHittableByKoppa,
        IHittableByFireBall
    {
        #region Objects
        [SerializeField] private GoombaProfile _profile;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private AudioSource _hitSoundFX;
        [SerializeField] private AudioSource _kickSoundFX;
        [SerializeField] private Animator _animator;
        private Movable _movable;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            Services.PlayerService.CanMoveChanged += OnCanMoveChanged;

            _movable = GetComponent<Movable>();
            _movable.Speed = _profile.MoveSpeed;
            _movable.Gravity = _profile.FallSpeed;
            _movable.MaxFallSpeed = _profile.MaxFallSpeed;
        }
        private void OnDestroy()
        {
            Services.PlayerService.CanMoveChanged -= OnCanMoveChanged;
        }
        #endregion

        #region Public Methods
        public void OnFall() => Destroy(gameObject);
        #endregion

        #region Private Methods
        private void Kill(Vector3 hitPosition)
        {
            _movable.ChekCollisions = false;
            gameObject.layer = 0; 

            _kickSoundFX.Play();
            _animator.SetTrigger("Kill");
            _renderer.sortingLayerName = "Dead";

            Services.ScoreService.Add(_profile.Points);
            Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.8f, 3f);

            if (Math.Sign(_movable.Speed) != Math.Sign(this.transform.position.x - hitPosition.x))
                _movable.Speed *= -1;

            _movable.AddJumpForce(_profile.JumpAcceleration);
        }
        private void Hit(PlayerController player)
        {
            gameObject.layer = 0; // Deshabilitado para otra colision
            _movable.enabled = false;

            _hitSoundFX.Play();
            enabled = false;
            _animator.SetTrigger("Hit");

            Services.ScoreService.Add(_profile.Points);
            Services.ScoreService.ShowPoints(_profile.Points, transform.position + Vector3.up * 2f, 0.5f, 1.5f);

            player.BounceJump();
            StartCoroutine(DestroyAfterHit());
        }
        private IEnumerator DestroyAfterHit()
        {
            yield return new WaitForSeconds(0.4f);
            Destroy(gameObject);
        }
        private void DamagePlayer(PlayerController player)
        {
            player.DamagePlayer();
        }
        private void ChangeDirectionToRight() => _movable.Speed = Mathf.Abs(_movable.Speed);
        private void ChangeDirectionToLeft() => _movable.Speed = -Mathf.Abs(_movable.Speed);
        #endregion

        #region Service Events
        private void OnCanMoveChanged() => _animator.speed = Services.PlayerService.CanMove ? 1 : 0;
        #endregion

        #region On local Ray Range Hit
        public void OnLeftCollided(RayHitInfo hitInfo) => ChangeDirectionToRight();
        public void OnRightCollided(RayHitInfo hitInfo) => ChangeDirectionToLeft();
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromTop(PlayerController player) => Hit(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => DamagePlayer(player);
        public void OnHittedByPlayerFromLeft(PlayerController player) => DamagePlayer(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => DamagePlayer(player);
        #endregion

        #region On Box Hit
        public void OnHittedByBox(GameObject box) => Kill(box.transform.position);
        #endregion

        #region On Koopa Hit
        public void OnHittedByKoppa(Koopa.Koopa koopa) => Kill(koopa.transform.position);
        #endregion

        #region On Fireball Hit
        public void OnHittedByFireBall(Fireball fireball) => Kill(fireball.transform.position);
        #endregion
    }
}