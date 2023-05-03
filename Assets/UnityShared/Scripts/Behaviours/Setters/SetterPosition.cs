using UnityEngine;

namespace UnityShared.Behaviours.Setters
{
    public class SetterPosition : SetterBase<Transform, Vector3>
    {
        protected override Transform OnGetComponent() => containerComponent == null ? transform : null;
        protected override void OnSetValue(Vector3 value) => base.containerComponent.position = value;
    }
}

