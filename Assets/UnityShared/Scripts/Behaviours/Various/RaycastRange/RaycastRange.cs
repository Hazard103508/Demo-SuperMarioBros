using Mono.Cecil;
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

        public UnityEvent<RayHitInfo> onHit;

        void Update()
        {
            CalculateCollision();
        }

        private RayRange CalculateRayRange() 
        {
            var start = (Vector2)transform.position + _profile.Range.StartPoint;
            var end = (Vector2)transform.position + _profile.Range.EndPoint;
            return new RayRange(start, end, _profile.Ray.Direction);
        }
        private void CalculateCollision()
        {
            var rayBound = CalculateRayRange();
            var hitInfo = new RayHitInfo()
            {
                IsBlock = CalculateCollisionDetection(rayBound, out List<GameObject> hits)
            };
            hitInfo.hitObjects = hits;

            onHit.Invoke(hitInfo);
        }
        private bool CalculateCollisionDetection(RayRange range, out List<GameObject> hits)
        {
            hits = new List<GameObject>();

            var _block = GetHitGameObjects(range, _profile.BlockLayers);
            hits.AddRange(_block);

            var _noBlock = GetHitGameObjects(range, _profile.OtherLayers);
            hits.AddRange(_noBlock);

            return _block.Any();
        }
        private List<GameObject> GetHitGameObjects(RayRange range, LayerMask layerMask)
        {
            return EvaluateRayPositions(range)
                .Select(point => Physics2D.Raycast(point, range.Dir, _profile.Ray.Length, layerMask))
                .Where(hit => hit.collider != null)
                .Select(hit => hit.collider.gameObject)
                .Distinct()
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