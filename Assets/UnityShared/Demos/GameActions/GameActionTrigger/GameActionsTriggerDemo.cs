using UnityEngine;

namespace UnityShared.Demos
{
    public class GameActionsTriggerDemo : MonoBehaviour
    {
        public void Print_OnTriggerEnter() => print("--TRIGGER ENTER--");
        public void Print_OnTriggerStay() => print("--TRIGGER STAY--");
        public void Print_OnTriggerExit() => print("--TRIGGER EXIT--");
    }
}