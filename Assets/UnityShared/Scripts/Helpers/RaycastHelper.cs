using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityShared.Helpers
{
    public static class RaycastHelper
    {
        /// <summary>
        /// Determines if the pointer is over a user interface object
        /// </summary>
        /// <returns></returns>
        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }
    }
}