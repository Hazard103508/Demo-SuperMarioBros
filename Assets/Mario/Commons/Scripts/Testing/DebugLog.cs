using System;
using UnityEngine;

namespace Mario.Commons.Testing
{
    public class DebugLog : MonoBehaviour
    {
        public bool useCustomColor;
        public Color logColor = Color.white;

        public void Log(string message) => Print(Debug.Log, message);
        public void LogWarning(string message) => Print(Debug.LogWarning, message);
        public void LogError(string message) => Print(Debug.LogError, message);

        private void Print(Action<object> logFunction, string message)
        {
            if (useCustomColor)
                logFunction($"<color={GetColorFormat()}>{message}</color>");
            else
                logFunction(message);
        }
        private string GetColorFormat() => string.Format("#{0:X2}{1:X2}{2:X2}", (byte)(logColor.r * 255f), (byte)(logColor.g * 255f), (byte)(logColor.b * 255f));
    }
}