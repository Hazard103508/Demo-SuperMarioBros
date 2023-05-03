using UnityEngine;

namespace UnityShared.Demos
{
    public class GameActionCollisionDemo : MonoBehaviour
    {
        public void Print_OnCollisionEnter() => print("--COLLSIION ENTER--");
        public void Print_OnCollisionStay() => print("--COLLISION STAY--");
        public void Print_OnCollisionExit() => print("--COLLISION EXIT--");
    }
}