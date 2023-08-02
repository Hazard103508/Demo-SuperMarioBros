using Mario.Application.Services;
using Mario.Game.Player;
using Mario.Game.ScriptableObjects.Items;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Npc
{
    public class Koopa : NPC
    {
        [SerializeField] private KoopaProfile _profile;
        private Coroutine _wakingUpCO;
        private bool _hitCoolDown;

        #region Unity Methods
        protected override void Awake()
        {
            base.Awake();
            State = KoopaStates.Idle;
            SetSpeed();
        }
        #endregion

        #region Public Properties
        public KoopaStates State { get; private set; }
        #endregion

        #region Protected Properties
        protected override float Profile_FallSpeed => _profile.FallSpeed;
        protected override float Profile_MaxFallSpeed => _profile.MaxFallSpeed;
        protected override int Profile_PointsKill => _profile.PointsKill;
        protected override float Profile_JumpAcceleration => _profile.JumpAcceleration;
        #endregion

        #region On Player Hit
        public override void OnHitFromTop(PlayerController player) => HitFromTop(player);
        public override void OnHitFromLeft(PlayerController player) => HitFromSide(player);
        public override void OnHitFromRight(PlayerController player) => HitFromSide(player);
        #endregion

        #region On Ray Range Hit
        public override void OnProximityRayHitLeft(RayHitInfo hitInfo) => HitToLeft(hitInfo);
        public override void OnProximityRayHitRight(RayHitInfo hitInfo) => HitToRight(hitInfo);
        #endregion

        #region Protected Methods
        protected override void CalculateWalk()
        {
            // BORRAR ------------------------------------------------------------------

            if (Input.GetKeyDown(KeyCode.I))
                Kill(transform.position - new Vector3(0.5f, 0, 0));
            if (Input.GetKeyDown(KeyCode.U))
                Kill(transform.position + new Vector3(0.5f, 0, 0));
            if (Input.GetKeyDown(KeyCode.O))
                HitFromTop(GameObject.FindObjectOfType<PlayerController>());
            // BORRAR ------------------------------------------------------------------

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
            if (State == KoopaStates.InShell)
                return;

            base.Move();
        }
        protected override void OnKill()
        {
            if (_wakingUpCO != null)
                StopCoroutine(_wakingUpCO);

            State = KoopaStates.Idle;
            _renderer.transform.position += Vector3.up * 0.5f;
        }
        #endregion

        #region Private Methods
        private void HitFromTop(PlayerController player)
        {
            if (_hitCoolDown)
                return;

            if (State == KoopaStates.InShell)
                SecondHit(player);
            else
                FirstHit(player);
        }
        private void HitFromSide(PlayerController player)
        {
            if (_hitCoolDown)
                return;

            if (State == KoopaStates.InShell)
                SecondHit(player);
            else
                DamagePlayer(player);
        }
        private void FirstHit(PlayerController player)
        {
            if (_isDead)
                return;

            _hitSoundFX.Play();
            _animator.SetTrigger("Hit");

            AllServices.ScoreService.Add(_profile.PointsHit1);
            AllServices.ScoreService.ShowPoint(_profile.PointsHit1, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

            State = KoopaStates.InShell;
            SetSpeed();
            _wakingUpCO = StartCoroutine(WakingUP());

            player.BounceJump();
            StartCoroutine(Cooldown());
        }
        private void SecondHit(PlayerController player)
        {
            if (_isDead)
                return;

            if (State == KoopaStates.InShell)
            {
                _animator.SetTrigger("Hit");

                if (_wakingUpCO != null)
                    StopCoroutine(_wakingUpCO);

                _kickSoundFX.Play();
                AllServices.ScoreService.Add(_profile.PointsHit2);
                AllServices.ScoreService.ShowPoint(_profile.PointsHit2, transform.position + Vector3.up * 1.5f, 0.5f, 1.5f);

                State = KoopaStates.Bouncing;
                SetSpeed();
                if (this.transform.position.x - player.transform.position.x < 0)
                    _currentSpeed.x = -Mathf.Abs(_currentSpeed.x);
                else
                    _currentSpeed.x = Mathf.Abs(_currentSpeed.x);

                StartCoroutine(Cooldown());
            }
        }
        private IEnumerator WakingUP()
        {
            yield return new WaitForSeconds(4f);
            _animator.SetTrigger("WakeUp");
            yield return new WaitForSeconds(1.5f);
            _animator.SetTrigger("Idle");

            State = KoopaStates.Idle;
        }
        private IEnumerator Cooldown()
        {
            _hitCoolDown = true;
            yield return new WaitForSeconds(.1f);
            _hitCoolDown = false;
        }
        private void SetSpeed()
        {
            float _direction = _renderer.flipX ? 1 : -1;
            _currentSpeed = Vector2.right * _direction * (State == KoopaStates.Bouncing ? Math.Abs(_profile.BouncingSpeed) : Math.Abs(_profile.MoveSpeed));
        }
        private void HitToLeft(RayHitInfo hitInfo)
        {
            KillNPC(hitInfo);
            _proximityBlock.left = hitInfo;
        }
        private void HitToRight(RayHitInfo hitInfo)
        {
            KillNPC(hitInfo);
            _proximityBlock.right = hitInfo;
        }
        private void KillNPC(RayHitInfo hitInfo)
        {
            if (State == KoopaStates.Bouncing)
            {
                if (!hitInfo.IsBlock)
                    return;

                var hitObj = hitInfo.hitObjects.Select(hit => new
                {
                    hit,
                    npc = hit.Object.GetComponent<NPC>()
                })
                .ToList();

                hitInfo.hitObjects = hitObj.Where(hit => hit.npc == null).Select(hit => hit.hit).ToList();
                hitInfo.IsBlock = hitInfo.hitObjects.Any();

                var npcHit = hitObj.Where(hit => hit.npc != null).Select(hit => hit).ToList();
                if (npcHit != null && npcHit.Any())
                    foreach (var hit in npcHit)
                    {
                        Vector3 hitPosition = hit.hit.Point;
                        if (hit.npc is Koopa && ((Koopa)hit.npc).State == KoopaStates.Bouncing)
                        {
                            hitPosition = new Vector3((hit.npc.transform.position.x + transform.position.x) / 2, hit.hit.Point.y);
                            this.Kill(hitPosition);
                        }

                        hit.npc.Kill(hitPosition);
                    }
            }
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