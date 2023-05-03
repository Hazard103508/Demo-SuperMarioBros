using UnityEngine.UI;

namespace UnityShared.Behaviours.Setters
{
    public class SetterRawImageSprite : SetterBase<RawImage, UnityEngine.Sprite>
    {
        protected override void OnSetValue(UnityEngine.Sprite value) => base.containerComponent.texture = value.texture;
    }
}

