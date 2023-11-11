using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Interactable
{
    public class TriggerPoint : MonoBehaviour,
        IHittableByPlayerFromLeft,
        IHittableByPlayerFromRight,
        IHittableByPlayerFromBottom,
        IHittableByPlayerFromTop
    {
        #region Objects
        public UnityEvent onTriggerOn;
#if UNITY_EDITOR
        [SerializeField] private Color _gizmoColor;
#endif
        [SerializeField] private bool _destroyOnTrigger;
        #endregion

        #region Unity Methods
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider2D>();

            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
        }
#endif
        #endregion

        #region Protected Methods
        protected virtual void OnHitCheckPoint(PlayerController player)
        {
            onTriggerOn.Invoke();
            if (_destroyOnTrigger)
                Destroy(gameObject);
        }
        #endregion

        #region On Player Hit
        public void OnHittedByPlayerFromLeft(PlayerController player) => OnHitCheckPoint(player);
        public void OnHittedByPlayerFromRight(PlayerController player) => OnHitCheckPoint(player);
        public void OnHittedByPlayerFromBottom(PlayerController player) => OnHitCheckPoint(player);
        public void OnHittedByPlayerFromTop(PlayerController player) => OnHitCheckPoint(player);
        #endregion
    }
}