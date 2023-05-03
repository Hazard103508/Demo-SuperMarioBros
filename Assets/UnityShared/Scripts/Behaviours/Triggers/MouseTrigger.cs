using UnityEngine;
using UnityEngine.UIElements;
using UnityShared.Enums;

namespace UnityShared.Behaviours.Triggers
{
    [AddComponentMenu("Event/Mouse Trigger")]
    public class MouseTrigger : BaseTrigger<MouseTriggerType, UnityShared.Behaviours.Triggers.MouseTrigger.MouseTriggerData>
    {
        private void OnMouseEnter() => Execute(MouseTriggerType.MOUSE_ENTER, GetMouseTriggerData());
        private void OnMouseExit() => Execute(MouseTriggerType.MOUSE_EXIT, GetMouseTriggerData());
        private void OnMouseUp() => Execute(MouseTriggerType.MOUSE_UP, GetMouseTriggerData());
        private void OnMouseOver()
        {
            var data = GetMouseTriggerData();
            Execute(MouseTriggerType.MOUSE_OVER, data);

            if (data.button.HasValue)
                Execute(MouseTriggerType.MOUSE_DOWN, data);
        }

        private MouseTriggerData GetMouseTriggerData()
        {
            return new MouseTriggerData()
            {
                gameObject = this.gameObject,
                button = Input.GetMouseButtonDown(0) ? MouseButton.LeftMouse :
                    Input.GetMouseButtonDown(1) ? MouseButton.RightMouse :
                    Input.GetMouseButtonDown(2) ? MouseButton.MiddleMouse :
                    null
            };
        }
        public class MouseTriggerData
        {
            public GameObject gameObject;
            public MouseButton? button;
        }

    }
}