using UnityEngine;

namespace UnityShared.Demos
{
    public class GameActionCollision2DDemo : MonoBehaviour
    {
        public void Print_OnCollisionEnter() => print("--COLLSTION 2D ENTER--");
        public void Print_OnCollisionStay() => print("--COLLISION 2D STAY--");
        public void Print_OnCollisionExit() => print("--COLLISION 2D EXIT--");
    }
}