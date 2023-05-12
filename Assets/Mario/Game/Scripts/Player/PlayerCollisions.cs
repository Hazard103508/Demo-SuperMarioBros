using Mario.Game.Boxes;
using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Items;
using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace Mario.Game.Player
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerCollisions : MonoBehaviour
    {
        #region Variables
        private Bounds<List<GameObject>> _proximityHit = new Bounds<List<GameObject>>();
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
                foreach (GameObject obj in _proximityHit.top)
                    HitObjectTop(obj);

            if (_proximityHit.bottom != null)
                foreach (GameObject obj in _proximityHit.bottom)
                    HitObjectBottom(obj);

            if (_proximityHit.left != null)
                foreach (GameObject obj in _proximityHit.left)
                    HitObjectLeft(obj);

            if (_proximityHit.right != null)
                foreach (GameObject obj in _proximityHit.right)
                    HitObjectRight(obj);
        }
        private bool HitObjectTop(GameObject obj)
        {
            ITopHitable script = obj.GetComponent<ITopHitable>();
            if (script != null)
            {
                script.OnHitTop(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectBottom(GameObject obj)
        {
            IBottomHitable script = obj.GetComponent<IBottomHitable>();
            if (script != null)
            {
                script.OnHitBottom(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectLeft(GameObject obj)
        {
            ILeftHitable script = obj.GetComponent<ILeftHitable>();
            if (script != null)
            {
                script.OnHitLeft(_playerController);
                return true;
            }

            return false;
        }
        private bool HitObjectRight(GameObject obj)
        {
            IRightHitable script = obj.GetComponent<IRightHitable>();
            if (script != null)
            {
                script.OnHitRight(_playerController);
                return true;
            }

            return false;
        }
    }
}