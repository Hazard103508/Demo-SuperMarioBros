using UnityEngine.UI;

namespace UnityShared.Behaviours.Setters
{
    public class SetterImageSprite : SetterBase<Image, UnityEngine.Sprite>
    {
        protected override void OnSetValue(UnityEngine.Sprite value) => base.containerComponent.sprite = value;
    }
}

