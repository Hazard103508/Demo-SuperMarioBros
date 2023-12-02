using Mario.Application.Interfaces;
using Mario.Application.Services;
using Mario.Game.ScriptableObjects.Pool;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class NpcCreator : MonoBehaviour
    {
        #region Objects
        private IPoolService _poolService;

#if UNITY_EDITOR
        [SerializeField] private Color _gizmoColor;
#endif  
        [SerializeField] private PooledObjectProfile _npcPoolReference;
        [SerializeField] private Vector2[] _targetLocations;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            foreach (Vector2 targetLocalPosition in _targetLocations)
            {
                Vector2 _position = GetTargetLocation(targetLocalPosition) + new Vector2(0.5f, 0.5f);
                Gizmos.DrawCube(_position, Vector2.one);
            }

        }
#endif
        #endregion

        #region Public Methods
        public void OnTriggerOn()
        {
            foreach (Vector2 targetLocalPosition in _targetLocations)
            {
                Vector2 _position = GetTargetLocation(targetLocalPosition);
                _poolService.GetObjectFromPool(_npcPoolReference, _position);
            }
        }
        #endregion

        #region Private Methods
        private Vector2 GetTargetLocation(Vector2 targetLocalPosition) => new Vector2(transform.position.x - transform.localPosition.x + targetLocalPosition.x, transform.position.y - transform.localPosition.y + targetLocalPosition.y);
        #endregion
    }
}