using Mario.Game.Enums;
using Mario.Game.Interfaces;
using Mario.Game.Props;
using UnityEngine;
using static Mario.Game.Player.PlayerController;
using UnityShared.Commons.Structs;
using System.Collections.Generic;

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
            EvaluateHit(_playerController);
        }

        #region On Ray Range Hit
        public void OnProximityRayHitLeft(RayHitInfo hitInfo) => _proximityHit.left = hitInfo.hitObjects;
        public void OnProximityRayHitRight(RayHitInfo hitInfo) => _proximityHit.right = hitInfo.hitObjects;
        public void OnProximityRayHitBottom(RayHitInfo hitInfo) => _proximityHit.bottom = hitInfo.hitObjects;
        public void OnProximityRayHitTop(RayHitInfo hitInfo) => _proximityHit.top = hitInfo.hitObjects;
        #endregion

        public void EvaluateHit(PlayerController playerController)
        {
            if (_proximityHit.top != null)
                foreach (GameObject obj in _proximityHit.top)
                {
                    if (HitObjectTop<Brick>(Tags.Brick, obj)) continue;

                }
        }
        private bool HitObjectTop<T>(Tags tag, GameObject obj) where T : MonoBehaviour, ITopHitable
        {
            if (obj.CompareTag(tag.ToString()))
            {
                T script = obj.GetComponent<T>();
                script.HitTop(_playerController);
                return true;
            }

            return false;
        }
    }
}