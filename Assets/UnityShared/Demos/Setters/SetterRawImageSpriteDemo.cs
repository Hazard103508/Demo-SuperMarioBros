using UnityShared.Behaviours.Setters;

namespace UnityShared.Demos
{
    public class SetterRawImageSpriteDemo : SetterRawImageSprite
    {
        private void OnEnable() => SettersDemoEvents.SetRawImageSprite += base.OnSetValue;
        private void OnDisable() => SettersDemoEvents.SetRawImageSprite -= base.OnSetValue;
    }
}