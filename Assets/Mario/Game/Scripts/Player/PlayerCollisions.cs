using Mario.Game.Interfaces;
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
        private bool HitObjectOnTop(HitObject hit)
        {
            if (hit == null)
                return false;

            IBottomHitable script = hit.Object.GetComponent<IBottomHitable>();
            if (script != null)
            {
                script.OnHitFromBottom(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectOnBottom(HitObject hit)
        {
            if (hit == null)
                return false;

            ITopHitable script = hit.Object.GetComponent<ITopHitable>();
            if (script != null)
            {
                script.OnHitFromTop(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectOnRight(HitObject hit)
        {
            if (hit == null)
                return false;

            ILeftHitable script = hit.Object.GetComponent<ILeftHitable>();
            if (script != null)
            {
                script.OnHitFromLeft(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectOnLeft(HitObject hit)
        {
            if (hit == null)
                return false;

            IRightHitable script = hit.Object.GetComponent<IRightHitable>();
            if (script != null)
            {
                script.OnHitFromRight(_playerController);
                return true;
            }

            return false;
        }
    }
}