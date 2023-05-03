using System;
using UnityEngine;

namespace UnityShared.Demos
{
    public class GameEventListenersDemoPrints : MonoBehaviour
    {
        public void PrintDefault() => Debug.Log("Demo action");
        public void PrintInt(int value) => Debug.Log($"Demo action int : {value}");
        public void PrintLong(long value) => Debug.Log($"Demo action long : {value}");
        public void PrintFloat(float value) => Debug.Log($"Demo action float : {value}");
        public void PrintDecimal(decimal value) => Debug.Log($"Demo action decimal : {value}");
        public void PrintDouble(double value) => Debug.Log($"Demo action double : {value}");
        public void PrintString(string value) => Debug.Log($"Demo action string : {value}");
        public void PrintBoolean(bool value) => Debug.Log($"Demo action bool : {value}");
        public void PrintDateTime(DateTime value) => Debug.Log($"Demo action datetime : {value}");
        public void PrintVector2(Vector2 value) => Debug.Log($"Demo action vector2 : {value}");
        public void PrintVector3(Vector3 value) => Debug.Log($"Demo action vector3 : {value}");
        public void PrintGameObject(GameObject value) => Debug.Log($"Demo action GameObject : {value.name}");
    }
}