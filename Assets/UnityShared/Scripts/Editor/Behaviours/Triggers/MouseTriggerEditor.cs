using UnityEditor;
using UnityShared.Behaviours.Triggers;
using UnityShared.Enums;

namespace UnityShared.Editor.Behaviours.Triggers
{
    [CustomEditor(typeof(MouseTrigger), true)]
    public class MouseTriggerEditor : BaseTriggerEditor<MouseTriggerType>
    {
    }
}