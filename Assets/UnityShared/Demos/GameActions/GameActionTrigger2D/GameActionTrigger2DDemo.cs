using UnityEngine;

namespace UnityShared.Demos
{
    public class GameActionTrigger2DDemo : MonoBehaviour
    {
        public void Print_OnTrigger2DEnter() => print("--TRIGGER 2D ENTER--");
        public void Print_OnTrigger2DStay() => print("--TRIGGER 2D STAY--");
        public void Print_OnTrigger2DExit() => print("--TRIGGER 2D EXIT--");
    }
}