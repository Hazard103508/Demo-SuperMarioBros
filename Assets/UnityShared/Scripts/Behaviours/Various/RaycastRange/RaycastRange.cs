using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.Structs;
using UnityShared.ScriptableObjects.GameObjects;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public abstract class RaycastRange : MonoBehaviour
    {
        [SerializeField] protected RaycastRangeProfile _profile;
        [HideInInspector] public Size2 SpriteSize;

        public UnityEvent<RayHitInfo> onHit;

        void Update()
        {
            CalculateCollision();
        }

        protected abstract RayRange CalculateRayRange();
        private void CalculateCollision()
        {
            var rayBound = CalculateRayRange();
            var hitInfo = new RayHitInfo()
            {
                IsHiting = CalculateCollisionDetection(rayBound, out List<GameObject> hits)
            };
            hitInfo.hitObjects = hits;

            onHit.Invoke(hitInfo);
        }
        private bool CalculateCollisionDetection(RayRange range, out List<GameObject> hits)
        {
            hits = EvaluateRayPositions(range)
                .Select(point => Physics2D.Raycast(point, range.Dir, _profile.DetectionRayLength, _profile.GroundLayer))
                .Where(hit => hit.collider != null)
                .Select(hit => hit.collider.gameObject)
                .ToList();

            return hits.Any();
        }
        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            if (_profile.RayCount == 1)
                yield return Vector2.Lerp(range.Start, range.End, 0.5f + _profile.OffSet);
            else
                for (var i = 0; i < _profile.RayCount; i++)
                {
                    var t = (float)i / (_profile.RayCount - 1);
                    yield return Vector2.Lerp(range.Start, range.End, t);
                }
        }

        private void OnDrawGizmos()
        {
            var rayRange = CalculateRayRange();
            Gizmos.color = Color.blue;
            foreach (var point in EvaluateRayPositions(rayRange))
                Gizmos.DrawRay(point, rayRange.Dir * _profile.DetectionRayLength);
        }
    }
}