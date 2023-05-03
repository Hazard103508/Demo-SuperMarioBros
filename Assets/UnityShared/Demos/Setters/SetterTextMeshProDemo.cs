using UnityShared.Behaviours.Setters;

namespace UnityShared.Demos
{
    public class SetterTextMeshProDemo : SetterTextMeshPro
    {
        private void OnEnable() => SettersDemoEvents.SetTextMeshPro += base.OnSetValue;
        private void OnDisable() => SettersDemoEvents.SetTextMeshPro -= base.OnSetValue;
    }
}