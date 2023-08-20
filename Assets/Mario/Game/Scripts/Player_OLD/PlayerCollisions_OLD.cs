using Mario.Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    [RequireComponent(typeof(PlayerController_OLD))]
    public class PlayerCollisions_OLD : MonoBehaviour
    {
        #region Variables
        private Bounds<List<HitObject>> _proximityHit = new Bounds<List<HitObject>>();
        private PlayerController_OLD _playerController;
        #endregion

        private void Awake()
        {
            _playerController = GetComponent<PlayerController_OLD>();
        }
        private void Update()
        {
            EvaluateHit();
        }

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityHit.left = hitInfo.hitObjects;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityHit.right = hitInfo.hitObjects;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityHit.bottom = hitInfo.hitObjects;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityHit.top = hitInfo.hitObjects;
        #endregion

        public void EvaluateHit()
        {
            if (_proximityHit.top != null)
            {
                foreach (HitObject hit in _proximityHit.top)
                    HitObjectOnTop(hit);
                _proximityHit.top.Clear();
            }

            if (_proximityHit.bottom != null)
            {
                foreach (HitObject hit in _proximityHit.bottom)
                    HitObjectOnBottom(hit);
                _proximityHit.bottom.Clear();
            }

            if (_proximityHit.left != null)
            {
                foreach (HitObject hit in _proximityHit.left)
                    HitObjectOnLeft(hit);
                _proximityHit.left.Clear();
            }

            if (_proximityHit.right != null)
            {
                foreach (HitObject hit in _proximityHit.right)
                    HitObjectOnRight(hit);
                _proximityHit.right.Clear();
            }
        }
        private bool HitObjectOnTop(HitObject hit) => HitObjectOn<IHittableByPlayerFromBottom>(hit, script => script.OnHittedByPlayerFromBottom(_playerController));
        private bool HitObjectOnBottom(HitObject hit) => HitObjectOn<IHittableByPlayerFromTop>(hit, script => script.OnHittedByPlayerFromTop(_playerController));
        private bool HitObjectOnRight(HitObject hit) => HitObjectOn<IHittableByPlayerFromLeft>(hit, script => script.OnHittedByPlayerFromLeft(_playerController));
        private bool HitObjectOnLeft(HitObject hit) => HitObjectOn<IHittableByPlayerFromRight>(hit, script => script.OnHittedByPlayerFromRight(_playerController));
        private bool HitObjectOn<T>(HitObject hit, Action<T> onHitFunc)
        {
            if (hit == null || hit.Object == null)
                return false;

            T script = hit.Object.GetComponent<T>();
            if (script != null)
            {
                onHitFunc.Invoke(script);
                return true;
            }

            return false;
        }
    }
}