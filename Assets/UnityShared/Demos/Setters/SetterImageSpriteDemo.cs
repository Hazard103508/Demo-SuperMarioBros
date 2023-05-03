using UnityShared.Behaviours.Setters;

namespace UnityShared.Demos
{
    public class SetterImageSpriteDemo : SetterImageSprite
    {
        private void OnEnable() => SettersDemoEvents.SetImageSprite += base.OnSetValue;
        private void OnDisable() => SettersDemoEvents.SetImageSprite -= base.OnSetValue;
    }
}