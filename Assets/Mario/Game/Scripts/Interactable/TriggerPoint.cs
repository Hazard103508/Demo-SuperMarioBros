using Mario.Game.Interfaces;
using Mario.Game.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Game.Interactable
{
    public class TriggerPoint : MonoBehaviour, ILeftHitable, IRightHitable, IBottomHitable, ITopHitable
    {
        [SerializeField] private Color _gizmoColor;
        public UnityEvent onTriggerOn;

        public void OnHitFromLeft(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromRight(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromBottom(PlayerController player) => OnHitCheckPoint(player);
        public void OnHitFromTop(PlayerController player) => OnHitCheckPoint(player);

        protected virtual void OnHitCheckPoint(PlayerController player) => onTriggerOn.Invoke();

        private void OnDrawGizmos()
        {
            var collider = GetComponent<BoxCollider2D>();

            Gizmos.color = _gizmoColor;
            Gizmos.DrawCube(transform.position + (Vector3)collider.offset, collider.size);
        }
    }
}