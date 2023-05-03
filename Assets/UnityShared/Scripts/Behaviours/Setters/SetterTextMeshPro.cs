using TMPro;

namespace UnityShared.Behaviours.Setters
{
    public class SetterTextMeshPro : SetterBase<TextMeshProUGUI, string>
    {
        protected override void OnSetValue(string value) => base.containerComponent.text = value;
    }
}

