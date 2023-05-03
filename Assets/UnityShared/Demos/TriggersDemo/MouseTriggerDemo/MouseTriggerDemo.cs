using UnityEngine;
using static UnityShared.Behaviours.Triggers.MouseTrigger;

namespace UnityShared.Demos
{
    public class MouseTriggerDemo : MonoBehaviour
    {
        public void OnMouseEnter(MouseTriggerData mouseTriggerData) => Print("Mouse Enter", mouseTriggerData);
        public void OnMouseExit(MouseTriggerData mouseTriggerData) => Print("Mouse Exit", mouseTriggerData);
        public void OnMouseUp(MouseTriggerData mouseTriggerData) => Print("Mouse Up", mouseTriggerData);
        public void OnMouseDown(MouseTriggerData mouseTriggerData) => Print("Mouse Down", mouseTriggerData);
        public void OnMouseOver(MouseTriggerData mouseTriggerData) => Print("Mouse Over", mouseTriggerData);
        public void OnMouseUpAsButton(MouseTriggerData mouseTriggerData) => Print("Mouse Up as Button", mouseTriggerData);

        private void Print(string action, MouseTriggerData mouseTriggerData)
        {
            if (mouseTriggerData.button.HasValue)
                Debug.Log($"{mouseTriggerData.gameObject.name} - {action} - {mouseTriggerData.button.Value}");
            else
                Debug.Log($"{mouseTriggerData.gameObject.name} - {action}");
        }
    }
}

