using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Interactable
{
    public class TriggerPoint : MonoBehaviour, IHitableByPlayerFromLeft, IHitableByPlayerFromRight, IHitableByPlayerFromBottom, IHitableByPlayerFromTop
    {
        #region Objects
        public UnityEvent onTriggerOn;
        [SerializeField] private Color _gizmoColor;
        [SerializeField] private bool _destroyOnTrigger;
        #endregion

        #region Unity Methods
        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider2D>();

            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
        }
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
        public void OnHitableByPlayerFromLeft(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitableByPlayerFromRight(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitableByPlayerFromBottom(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitableByPlayerFromTop(PlayerController player) => OnHitCheckPoint(player);
        #endregion
    }
}