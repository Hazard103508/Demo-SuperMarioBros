using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Npc
{
    public class Koopa : NPC
    {
        [SerializeField] private KoopaProfile _profile;
        private KoopaStates _state;
        private Coroutine _wakingUpCO;

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            _state = KoopaStates.Idle;
            SetSpeed();
        }
        #endregion

        #region Protected Properties
        protected override float Profile_FallSpeed => _profile.FallSpeed;
        protected override float Profile_MaxFallSpeed => _profile.MaxFallSpeed;
        protected override int Profile_PointsHit => _profile.PointsHit;
        protected override int Profile_PointsKill => _profile.PointsKill;
        protected override float Profile_JumpAcceleration => _profile.JumpAcceleration;
        #endregion

        #region On Player Hit
        public override void OnHitFromTop(PlayerController player) => HitFromTop(player);
        public override void OnHitFromLeft(PlayerController player) => HitFromSide(player);
        public override void OnHitFromRight(PlayerController player) => HitFromSide(player);
        #endregion

        #region Protected Methods
        protected override void CalculateWalk()
        {
            // borrar ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------
            if (Input.GetKeyDown(KeyCode.K))
                HitFromTop(GameObject.FindObjectOfType<PlayerController>());
            // borrar ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ---------- ----------

            if (_proximityBlock.right.IsBlock)
            {
                _renderer.flipX = false;
                _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
            }
            else if (_proximityBlock.left.IsBlock)
            {
                _renderer.flipX = true;
                _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
            }
        }
        protected override void Move()
        {
            if (_state == KoopaStates.InShell)
                return;

            base.Move();
        }
        protected override void OnKill()
        {
            //_renderer.transform.position -= Vector3.up * 0.5f;
        }
        #endregion

        #region Private Methods
        private void HitFromTop(PlayerController player)
        {
            if (_state == KoopaStates.InShell)
                SecondHit(player);
            else
                FirstHit(player);
        }
        private void HitFromSide(PlayerController player)
        {
            if (_state == KoopaStates.InShell)
                SecondHit(player);
            else
                DamagePlayer(player);
        }
        private void FirstHit(PlayerController player)
        {
            if (_isDead)
                return;

            _state = KoopaStates.InShell;
            SetSpeed();
            _wakingUpCO = StartCoroutine(WakingUP());

            _hitSoundFX.Play();
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(Profile_PointsHit);
            AllServices.ScoreService.ShowPoint(Profile_PointsHit, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            player.BounceJump();
        }
        private void SecondHit(PlayerController player)
        {
            if (_isDead)
                return;

            if (_state == KoopaStates.InShell)
            {
                if (_wakingUpCO != null)
                    StopCoroutine(_wakingUpCO);

                _state = KoopaStates.Bouncing;
                SetSpeed();
                if (Math.Sign(_currentSpeed.x) != Math.Sign(this.transform.position.x - player.transform.position.x))
                    _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
                else
                    _currentSpeed.x = Mathf.Abs(_currentSpeed.x);
            }
        }
        private IEnumerator WakingUP()
        {
            yield return new WaitForSeconds(4f);
            _animator.SetTrigger("WakeUp");
            yield return new WaitForSeconds(1.5f);
            _animator.SetTrigger("Idle");

            _state = KoopaStates.Idle;
        }
        private void SetSpeed()
        {
            _currentSpeed = Vector2.right * (_state == KoopaStates.Idle ? _profile.MoveSpeed : _profile.BouncingSpeed);
        }
        #endregion

        #region Structures
        public enum KoopaStates
        {
            Idle,
            InShell,
            Bouncing,
        }
        #endregion
    }
}