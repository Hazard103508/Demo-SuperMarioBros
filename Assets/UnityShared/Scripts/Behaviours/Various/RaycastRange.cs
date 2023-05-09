using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;
using UnityShared.Enums;
using System.Linq;
using UnityEngine.Events;
using UnityShared.ScriptableObjects.GameObjects;

namespace UnityShared.Behaviours.Various
{
    public class RaycastRange : MonoBehaviour
    {
        [SerializeField] private RaycastRangeProfile _profile;
        public Vector2 SpriteSize;

        public UnityEvent<RayHitInfo> onHit;

        void Update()
        {
            CalculateCollision();
        }

        private RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return _profile.Bound switch
            {
                BoundTypes.LEFT => new RayRange(b.min.x, b.min.y + _profile.OffSet, b.min.x, b.max.y - _profile.OffSet, Vector2.left),
                BoundTypes.RIGHT => new RayRange(b.max.x, b.min.y + _profile.OffSet, b.max.x, b.max.y - _profile.OffSet, Vector2.right),
                BoundTypes.TOP => new RayRange(b.min.x + _profile.OffSet, b.max.y, b.max.x - _profile.OffSet, b.max.y, Vector2.up),
                BoundTypes.BOTTOM => new RayRange(b.min.x + _profile.OffSet, b.min.y, b.max.x - _profile.OffSet, b.min.y, Vector2.down),
                _ => throw new System.NotImplementedException(),
            };
        }
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