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

        [SerializeField] private PooledObjectProfile _npcPoolReference;
        [SerializeField] private Vector2[] _targetLocations;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        #endregion

        #region Public Methods
        public void OnTriggerOn()
        {
            foreach (Vector2 initPosition in _targetLocations)
            {
                _poolService.GetObjectFromPool(_npcPoolReference, initPosition);
            }
        }
        #endregion
    }
}