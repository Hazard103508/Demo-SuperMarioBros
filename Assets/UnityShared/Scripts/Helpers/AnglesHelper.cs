using UnityEngine;

namespace UnityShared.Helpers
{
    public static class AnglesHelper
    {
        /// <summary>
        /// Converts an angle in radians to Vector2
        /// </summary>
        /// <param name="radian"></param>
        /// <returns></returns>
        public static Vector2 ConvertRadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }
        /// <summary>
        /// Converts an angle in degrees to Vector2
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static Vector2 ConvertDegreesToVector2(float degree)
        {
            return ConvertRadianToVector2(degree * Mathf.Deg2Rad);
        }

        public static float Clamp(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f)
                lfAngle += 360f;

            if (lfAngle > 360f)
                lfAngle -= 360f;

            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}