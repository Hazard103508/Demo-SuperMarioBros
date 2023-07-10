using Mario.Game.Interfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisions : MonoBehaviour
    {
        #region Variables
        private Bounds<List<HitObject>> _proximityHit = new Bounds<List<HitObject>>();
        private PlayerController _playerController;
        #endregion

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
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
                foreach (HitObject hit in _proximityHit.top)
                    HitObjectOnTop(hit);

            if (_proximityHit.bottom != null)
                foreach (HitObject hit in _proximityHit.bottom)
                    HitObjectOnBottom(hit);

            if (_proximityHit.left != null)
                foreach (HitObject hit in _proximityHit.left)
                    HitObjectOnLeft(hit);

            if (_proximityHit.right != null)
                foreach (HitObject hit in _proximityHit.right)
                    HitObjectOnRight(hit);
        }
        private bool HitObjectOnTop(HitObject hit) => HitObjectOn<IBottomHitable>(hit, script => script.OnHitFromBottom(_playerController));
        private bool HitObjectOnBottom(HitObject hit) => HitObjectOn<ITopHitable>(hit, script => script.OnHitFromTop(_playerController));
        private bool HitObjectOnRight(HitObject hit) => HitObjectOn<ILeftHitable>(hit, script => script.OnHitFromLeft(_playerController));
        private bool HitObjectOnLeft(HitObject hit) => HitObjectOn<IRightHitable>(hit, script => script.OnHitFromRight(_playerController));
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