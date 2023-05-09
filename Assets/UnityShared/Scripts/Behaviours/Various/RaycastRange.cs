using System.Collections.Generic;
using UnityEngine;
using UnityShared.Commons.Structs;
using UnityShared.Enums;
using System.Linq;
using UnityEngine.Events;

namespace UnityShared.Behaviours.Various
{
    public class RaycastRange : MonoBehaviour
    {
        [SerializeField] private BoundTypes _bound;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _offSet;
        [SerializeField] private int _rayCount;
        [SerializeField] private float _detectionRayLength = 0.1f;
        public Vector2 SpriteSize;

        public UnityEvent<RayHitInfo> onHit;

        void Update()
        {
            CalculateCollision();
        }

        private RayRange CalculateRayRange()
        {
            var b = new Bounds(transform.position, SpriteSize);
            return _bound switch
            {
                BoundTypes.LEFT => new RayRange(b.min.x, b.min.y + _offSet, b.min.x, b.max.y - _offSet, Vector2.left),
                BoundTypes.RIGHT => new RayRange(b.max.x, b.min.y + _offSet, b.max.x, b.max.y - _offSet, Vector2.right),
                BoundTypes.TOP => new RayRange(b.min.x + _offSet, b.max.y, b.max.x - _offSet, b.max.y, Vector2.up),
                BoundTypes.BOTTOM => new RayRange(b.min.x + _offSet, b.min.y, b.max.x - _offSet, b.min.y, Vector2.down),
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
                .Select(point => Physics2D.Raycast(point, range.Dir, _detectionRayLength, _groundLayer))
                .Where(hit => hit.collider != null)
                .Select(hit => hit.collider.gameObject)
                .ToList();

            return hits.Any();
        }
        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            for (var i = 0; i < _rayCount; i++)
            {
                var t = (float)i / (_rayCount - 1);
                yield return Vector2.Lerp(range.Start, range.End, t);
            }
        }
    }
}