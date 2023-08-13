using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityShared.Commons.Structs;
using UnityShared.ScriptableObjects.GameObjects;

namespace UnityShared.Behaviours.Various.RaycastRange
{
    public class RaycastRange : MonoBehaviour
    {
        [SerializeField] protected RaycastRangeProfile _profile;
        public bool runAutomatically = true;
        public UnityEvent<RayHitInfo> onHit;

        private void Update()
        {
            if (runAutomatically)
                CalculateCollision();
        }

        [Obsolete("Method1 is deprecated")]
        public void CalculateCollision()
        {
            var rayBound = CalculateRayRange();
            var hitInfo = new RayHitInfo()
            {
                IsBlock = CalculateCollisionDetection(rayBound, out List<HitObject> hits)
            };
            hitInfo.hitObjects = hits;
            onHit.Invoke(hitInfo);
        }
        public RayHitInfo CalculateCollision(float rayLength)
        {
            _profile.Ray.Length = rayLength;
            var rayBound = CalculateRayRange();
            var hitInfo = new RayHitInfo()
            {
                IsBlock = CalculateCollisionDetection(rayBound, out List<HitObject> hits)
            };
            hitInfo.hitObjects = hits;
            return hitInfo;
        }

        private RayRange CalculateRayRange()
        {
            var position = (Vector2)transform.position;
            var start = position + _profile.Range.StartPoint;
            var end = position + _profile.Range.EndPoint;
            return new RayRange(start, end, _profile.Ray.Direction);
        }
        private bool CalculateCollisionDetection(RayRange range, out List<HitObject> hits)
        {
            hits = new List<HitObject>();

            var _block = GetHitObject(range, _profile.BlockLayers);
            hits.AddRange(_block);

            var _noBlock = GetHitObject(range, _profile.OtherLayers);
            hits.AddRange(_noBlock);

            return _block.Any();
        }
        private List<HitObject> GetHitObject(RayRange range, LayerMask layerMask)
        {
            if (layerMask.value == 0)
                return new List<HitObject>();

            var hits = GetRaycastHit(range, layerMask);

            var relativePosition = new Vector2(
                (_profile.Range.EndPoint.x + _profile.Range.StartPoint.x) / 2,
                (_profile.Range.EndPoint.y + _profile.Range.StartPoint.y) / 2
            ) + (_profile.Ray.Direction * (_profile.Ray.Length + 0.001f));

            return hits
                .GroupBy(h => h.collider.gameObject)
                .Select(h => new HitObject()
                {
                    Object = h.Key,
                    Point = new Vector2(h.Average(x => x.point.x), h.Average(x => x.point.y)),
                    RelativePosition = relativePosition
                })
                .ToList();
        }
        private List<RaycastHit2D> GetRaycastHit(RayRange range, LayerMask layerMask)
        {
            return EvaluateRayPositions(range)
                .Select(point => Physics2D.Raycast(point, range.Dir, _profile.Ray.Length, layerMask))
                .Where(hit => hit.collider != null && hit.collider.gameObject.activeSelf)
                .ToList();
        }
        private IEnumerable<Vector2> EvaluateRayPositions(RayRange range)
        {
            if (_profile.Range.Count == 1)
                yield return Vector2.Lerp(range.Start, range.End, 0.5f);
            else
                for (var i = 0; i < _profile.Range.Count; i++)
                {
                    var t = (float)i / (_profile.Range.Count - 1);
                    yield return Vector2.Lerp(range.Start, range.End, t);
                }
        }
        private void OnDrawGizmos()
        {
            var rayRange = CalculateRayRange();
            Gizmos.color = Color.blue;
            foreach (var point in EvaluateRayPositions(rayRange))
                Gizmos.DrawRay(point, rayRange.Dir * _profile.Ray.Length);
        }
    }
}