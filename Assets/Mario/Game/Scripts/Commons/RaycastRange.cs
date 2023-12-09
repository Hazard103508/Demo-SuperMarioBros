using Mario.Commons.ScriptableObjects;
using Mario.Commons.Structs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mario.Game.Commons
{
    public class RaycastRange : MonoBehaviour
    {
        #region Objects
        [SerializeField] protected RaycastRangeProfile _profile;
        private float _rayExtraLength;
        #endregion

        #region Properties
        public RaycastRangeProfile Profile
        {
            get => _profile;
            set => _profile = value;
        }
        #endregion

        #region Public Methods
        public RayHitInfo CalculateCollision(float rayExtraLength)
        {
            _rayExtraLength = rayExtraLength;
            var rayBound = CalculateRayRange();
            CalculateCollisionDetection(rayBound, out List<HitObject> hits);

            var hitInfo = new RayHitInfo();
            hitInfo.hitObjects = hits;
            hitInfo.IsBlock = hitInfo.hitObjects.Any(obj => obj.IsBlock);
            return hitInfo;
        }
        #endregion

        #region Private Methods
        private RayRange CalculateRayRange()
        {
            var position = (Vector2)transform.position;
            var start = position + _profile.Range.StartPoint;
            var end = position + _profile.Range.EndPoint;
            return new RayRange(start, end, _profile.Ray.Direction);
        }
        private void CalculateCollisionDetection(RayRange range, out List<HitObject> hits)
        {
            hits = new();
            hits.AddRange(GetHitObject(range, _profile.BlockLayers, true));
            hits.AddRange(GetHitObject(range, _profile.OtherLayers, false));
        }
        private List<HitObject> GetHitObject(RayRange range, LayerMask layerMask, bool isBlock)
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
                    IsBlock = isBlock,
                    Object = h.Key,
                    Point = new Vector2(h.Average(x => x.point.x), h.Average(x => x.point.y)),
                    RelativePosition = relativePosition
                })
                .ToList();
        }
        private List<RaycastHit2D> GetRaycastHit(RayRange range, LayerMask layerMask)
        {
            return EvaluateRayPositions(range)
                .Select(point => Physics2D.Raycast(point, range.Dir, _profile.Ray.Length + _rayExtraLength, layerMask))
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
        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var rayRange = CalculateRayRange();
            Gizmos.color = Color.blue;
            foreach (var point in EvaluateRayPositions(rayRange))
                Gizmos.DrawRay(point, rayRange.Dir * _profile.Ray.Length);
        }
#endif
    }
}