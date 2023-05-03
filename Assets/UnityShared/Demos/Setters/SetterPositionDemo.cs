using UnityShared.Behaviours.Setters;

namespace UnityShared.Demos
{
    public class SetterPositionDemo : SetterPosition
    {
        private void OnEnable() => SettersDemoEvents.SetPosition += base.OnSetValue;
        private void OnDisable() => SettersDemoEvents.SetPosition -= base.OnSetValue;
    }
}