using UnityShared.Behaviours.Setters;

namespace UnityShared.Demos
{
    public class SetterRotationDemo : SetterRotation
    {
        private void OnEnable() => SettersDemoEvents.SetRotation += base.OnSetValue;
        private void OnDisable() => SettersDemoEvents.SetRotation -= base.OnSetValue;
    }
}

