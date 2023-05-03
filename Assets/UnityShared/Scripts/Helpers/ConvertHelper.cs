using System;
using UnityEngine;

namespace UnityShared.Helpers
{
    public static class ConvertHelper
    {
        #region ToInt
        public static int ToInt(sbyte value) => System.Convert.ToInt32(value);
        public static int ToInt(byte value) => System.Convert.ToInt32(value);
        public static int ToInt(short value) => System.Convert.ToInt32(value);
        public static int ToInt(ushort value) => System.Convert.ToInt32(value);
        public static int ToInt(uint value)
        {
            if (value > int.MaxValue)
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            else
                return System.Convert.ToInt32(value & 0xffffffff);
        }
        public static int ToInt(long value)
        {
            if (value > int.MaxValue)
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            else
                return System.Convert.ToInt32(value & 0xffffffff);
        }
        public static int ToInt(ulong value)
        {
            if (value > int.MaxValue)
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            else
                return System.Convert.ToInt32(value & 0xffffffff);
        }
        public static int ToInt(float value) => System.Convert.ToInt32(value);
        public static int ToInt(double value) => System.Convert.ToInt32(value);
        public static int ToInt(decimal value) => System.Convert.ToInt32(value);
        public static int ToInt(bool value) => value ? 1 : 0;
        public static int ToInt(char value) => System.Convert.ToInt32(value);
        public static int ToInt(object value)
        {
            if (value == null)
                return 0;

            else if (value is Color)
                return ToInt((Color)value);

            else if (value is Color32)
                return ToInt((Color32)value);

            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            else
                return ToInt(value.ToString());
        }
        public static int ToInt(string value, System.Globalization.NumberStyles style) => ToInt(ToDouble(value, style));
        public static int ToInt(string value) => ToInt(ToDouble(value, System.Globalization.NumberStyles.Any));
        #endregion

        #region ToLong
        public static long ToLong(sbyte value) => System.Convert.ToInt64(value);
        public static long ToLong(byte value) => System.Convert.ToInt64(value);
        public static long ToLong(short value) => System.Convert.ToInt64(value);
        public static long ToLong(ushort value) => System.Convert.ToInt64(value);
        public static long ToLong(int value) => System.Convert.ToInt64(value);
        public static long ToLong(uint value) => System.Convert.ToInt64(value);
        public static long ToLong(ulong value)
        {
            if (value > long.MaxValue)
            {
                return int.MinValue + System.Convert.ToInt32(value & long.MaxValue);
            }
            else
            {
                return System.Convert.ToInt64(value & long.MaxValue);
            }
        }
        public static long ToLong(float value) => System.Convert.ToInt64(value);
        public static long ToLong(double value) => System.Convert.ToInt64(value);
        public static long ToLong(decimal value) => System.Convert.ToInt64(value);
        public static long ToLong(bool value) => value ? 1 : 0;
        public static long ToLong(char value) => System.Convert.ToInt64(value);
        public static long ToLong(object value)
        {
            if (value == null)
                return 0;
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToInt64(value);
                }
                catch
                {
                    return 0;
                }
            }
            else
                return ToLong(value.ToString());
        }
        public static long ToLong(string value, System.Globalization.NumberStyles style) => ToLong(ToDouble(value, style));
        public static long ToLong(string value) => ToLong(ToDouble(value, System.Globalization.NumberStyles.Any));
        #endregion

        #region ToDouble
        public static double ToDouble(sbyte value) => System.Convert.ToDouble(value);
        public static double ToDouble(byte value) => System.Convert.ToDouble(value);
        public static double ToDouble(short value) => System.Convert.ToDouble(value);
        public static double ToDouble(ushort value) => System.Convert.ToDouble(value);
        public static double ToDouble(int value) => System.Convert.ToDouble(value);
        public static double ToDouble(uint value) => System.Convert.ToDouble(value);
        public static double ToDouble(long value) => System.Convert.ToDouble(value);
        public static double ToDouble(ulong value) => System.Convert.ToDouble(value);
        public static double ToDouble(float value) => System.Convert.ToDouble(value);
        public static double ToDouble(decimal value) => System.Convert.ToDouble(value);
        public static double ToDouble(bool value) => value ? 1 : 0;
        public static double ToDouble(char value) => ToDouble(System.Convert.ToInt32(value));
        public static double ToDouble(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToDouble(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
            {
                return ToDouble((Vector2)value);
            }
            else if (value is Vector3)
            {
                return ToDouble((Vector3)value);
            }
            else if (value is Vector4)
            {
                return ToDouble((Vector3)value);
            }
            else
            {
                return ToDouble(value.ToString(), System.Globalization.NumberStyles.Any, null);
            }
        }
        public static double ToDouble(string value, System.Globalization.NumberStyles style) => ToDouble(value, style, null);
        public static double ToDouble(string value) => ToDouble(value, System.Globalization.NumberStyles.Any, null);
        /// <summary>
        /// System.Converts any string to a number with no errors.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        /// <remarks>
        /// TODO: I would also like to possibly include support for other number system bases. At least binary and octal.
        /// </remarks>
        public static double ToDouble(string value, System.Globalization.NumberStyles style, System.IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value)) return 0d;

            style = style & System.Globalization.NumberStyles.Any;
            double dbl = 0;
            if (double.TryParse(value, style, provider, out dbl))
            {
                return dbl;
            }
            else
            {
                //test hex
                int i;
                bool isNeg = false;
                for (i = 0; i < value.Length; i++)
                {
                    if (value[i] == ' ' || value[i] == '+') continue;
                    if (value[i] == '-')
                    {
                        isNeg = !isNeg;
                        continue;
                    }
                    break;
                }

                if (i < value.Length - 1 &&
                        (
                        (value[i] == '#') ||
                        (value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X')) ||
                        (value[i] == '&' && (value[i + 1] == 'h' || value[i + 1] == 'H'))
                        ))
                {
                    //is hex
                    style = (style & System.Globalization.NumberStyles.HexNumber) | System.Globalization.NumberStyles.AllowHexSpecifier;

                    if (value[i] == '#') i++;
                    else i += 2;
                    int j = value.IndexOf('.', i);

                    if (j >= 0)
                    {
                        long lng = 0;
                        long.TryParse(value.Substring(i, j - i), style, provider, out lng);

                        if (isNeg)
                            lng = -lng;

                        long flng = 0;
                        string sfract = value.Substring(j + 1).Trim();
                        long.TryParse(sfract, style, provider, out flng);
                        return System.Convert.ToDouble(lng) + System.Convert.ToDouble(flng) / System.Math.Pow(16d, sfract.Length);
                    }
                    else
                    {
                        string num = value.Substring(i);
                        long l;
                        if (long.TryParse(num, style, provider, out l))
                            return System.Convert.ToDouble(l);
                        else
                            return 0d;
                    }
                }
                else
                {
                    return 0d;
                }
            }
        }
        #endregion

        #region ToDecimal
        public static decimal ToDecimal(sbyte value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(byte value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(short value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(ushort value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(int value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(uint value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(long value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(ulong value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(float value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(double value) => System.Convert.ToDecimal(value);
        public static decimal ToDecimal(bool value) => value ? 1 : 0;
        public static decimal ToDecimal(char value) => ToDecimal(System.Convert.ToInt32(value));
        public static decimal ToDecimal(Vector2 value) => (decimal)value.x;
        public static decimal ToDecimal(Vector3 value) => (decimal)value.x;
        public static decimal ToDecimal(Vector4 value) => (decimal)value.x;
        public static decimal ToDecimal(object value)
        {
            if (value == null)
                return 0;

            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToDecimal(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
                return ToDecimal((Vector2)value);
            else if (value is Vector3)
                return ToDecimal((Vector3)value);
            else if (value is Vector4)
                return ToDecimal((Vector3)value);
            else
                return ToDecimal(value.ToString());
        }

        public static decimal ToDecimal(string value, System.Globalization.NumberStyles style) => System.Convert.ToDecimal(ToDouble(value, style));
        public static decimal ToDecimal(string value) => System.Convert.ToDecimal(ToDouble(value, System.Globalization.NumberStyles.Any));
        #endregion

        #region ToFloat
        public static float ToFloat(sbyte value) => System.Convert.ToSingle(value);
        public static float ToFloat(byte value) => System.Convert.ToSingle(value);
        public static float ToFloat(short value) => System.Convert.ToSingle(value);
        public static float ToFloat(ushort value) => System.Convert.ToSingle(value);
        public static float ToFloat(int value) => System.Convert.ToSingle(value);
        public static float ToFloat(uint value) => System.Convert.ToSingle(value);
        public static float ToFloat(long value) => System.Convert.ToSingle(value);
        public static float ToFloat(ulong value) => System.Convert.ToSingle(value);
        public static float ToFloat(double value) => (float)value;
        public static float ToFloat(decimal value) => System.Convert.ToSingle(value);
        public static float ToFloat(bool value) => value ? 1 : 0;
        public static float ToFloat(char value) => ToFloat(System.Convert.ToInt32(value));
        public static float ToFloat(object value)
        {
            if (value == null)
                return 0;
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToSingle(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
                return ToFloat((Vector2)value);
            else if (value is Vector3)
                return ToFloat((Vector3)value);
            else if (value is Vector4)
                return ToFloat((Vector3)value);
            else
                return ToFloat(value.ToString());
        }
        public static float ToFloat(string value, System.Globalization.NumberStyles style) => System.Convert.ToSingle(ToDouble(value, style));
        public static float ToFloat(string value) => System.Convert.ToSingle(ToDouble(value, System.Globalization.NumberStyles.Any));
        #endregion

        #region ToBool
        public static bool ToBool(sbyte value) => value != 0;
        public static bool ToBool(byte value) => value != 0;
        public static bool ToBool(short value) => value != 0;
        public static bool ToBool(ushort value) => value != 0;
        public static bool ToBool(int value) => value != 0;
        public static bool ToBool(uint value) => value != 0;
        public static bool ToBool(long value) => value != 0;
        public static bool ToBool(ulong value) => value != 0;
        public static bool ToBool(float value) => value != 0;
        public static bool ToBool(double value) => value != 0;
        public static bool ToBool(decimal value) => value != 0;
        public static bool ToBool(char value) => System.Convert.ToInt32(value) != 0;
        public static bool ToBool(object value)
        {
            if (value == null)
                return false;
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToBoolean(value);
                }
                catch
                {
                    return false;
                }
            }
            else
                return ToBool(value.ToString());
        }
        public static bool ToBool(string str) =>
            !(string.IsNullOrEmpty(str) ||
            str.Equals("false", System.StringComparison.OrdinalIgnoreCase) ||
            str.Equals("0", System.StringComparison.OrdinalIgnoreCase) ||
            str.Equals("off", System.StringComparison.OrdinalIgnoreCase));
        #endregion

        #region ToString
        public static string ToString(sbyte value) => System.Convert.ToString(value);
        public static string ToString(byte value) => System.Convert.ToString(value);
        public static string ToString(short value) => System.Convert.ToString(value);
        public static string ToString(ushort value) => System.Convert.ToString(value);
        public static string ToString(int value) => System.Convert.ToString(value);
        public static string ToString(uint value) => System.Convert.ToString(value);
        public static string ToString(long value) => System.Convert.ToString(value);
        public static string ToString(ulong value) => System.Convert.ToString(value);
        public static string ToString(float value) => System.Convert.ToString(value);
        public static string ToString(double value) => System.Convert.ToString(value);
        public static string ToString(decimal value) => System.Convert.ToString(value);
        public static string ToString(bool value, string sFormat)
        {
            switch (sFormat)
            {
                case "num":
                    return (value) ? "1" : "0";
                case "normal":
                case "":
                case null:
                    return System.Convert.ToString(value);
                default:
                    return System.Convert.ToString(value);
            }
        }
        public static string ToString(bool value) => System.Convert.ToString(value);
        public static string ToString(char value) => System.Convert.ToString(value);
        public static string ToString(object value) => System.Convert.ToString(value);
        #endregion

        #region ToColor
        public static Color ToColor(int value)
        {
            var a = (float)(value >> 24 & 0xFF) / 255f;
            var r = (float)(value >> 16 & 0xFF) / 255f;
            var g = (float)(value >> 8 & 0xFF) / 255f;
            var b = (float)(value & 0xFF) / 255f;
            return new Color(r, g, b, a);
        }
        public static Color ToColor(string value) => ToColor(ToInt(value));
        public static Color ToColor(Color32 value) => new Color((float)value.r / 255f, (float)value.g / 255f, (float)value.b / 255f, (float)value.a / 255f);
        public static Color ToColor(Vector3 value) => new Color((float)value.x, (float)value.y, (float)value.z);
        public static Color ToColor(Vector4 value) => new Color((float)value.x, (float)value.y, (float)value.z, (float)value.w);
        public static Color ToColor(object value)
        {
            if (value is Color) return (Color)value;
            if (value is Color32) return ToColor((Color32)value);
            if (value is Vector3) return ToColor((Vector3)value);
            if (value is Vector4) return ToColor((Vector4)value);
            return ToColor(ToInt(value));
        }
        public static Color32 ToColor32(int value)
        {
            byte a = (byte)(value >> 24 & 0xFF);
            byte r = (byte)(value >> 16 & 0xFF);
            byte g = (byte)(value >> 8 & 0xFF);
            byte b = (byte)(value & 0xFF);
            return new Color32(r, g, b, a);
        }
        public static Color32 ToColor32(string value) => ToColor32(ToInt(value));
        public static Color32 ToColor32(Color value) => new Color32((byte)(value.r * 255f), (byte)(value.g * 255f), (byte)(value.b * 255f), (byte)(value.a * 255f));
        public static Color32 ToColor32(Vector3 value) => new Color32((byte)(value.x * 255f), (byte)(value.y * 255f), (byte)(value.z * 255f), 255);
        public static Color32 ToColor32(Vector4 value) => new Color32((byte)(value.x * 255f), (byte)(value.y * 255f), (byte)(value.z * 255f), (byte)(value.w * 255f));
        public static Color32 ToColor32(object value)
        {
            if (value is Color32) return (Color32)value;
            if (value is Color) return ToColor32((Color)value);
            if (value is Vector3) return ToColor32((Vector3)value);
            if (value is Vector4) return ToColor32((Vector4)value);
            return ToColor32(ToInt(value));
        }
        #endregion

        #region ToVector2
        public static Vector2 ToVector2(float value) => new Vector2(value, value);
        public static Vector2 ToVector2(Vector3 vec) => new Vector2(vec.x, vec.y);
        public static Vector2 ToVector2(Vector4 vec) => new Vector2(vec.x, vec.y);
        public static Vector2 ToVector2(Quaternion vec) => new Vector2(vec.x, vec.y);
        public static Vector2 ToVector2(object value)
        {
            if (value == null) return Vector2.zero;
            if (value is Vector2) return (Vector2)value;
            if (value is Vector3)
            {
                var v = (Vector3)value;
                return new Vector2(v.x, v.y);
            }
            if (value is Vector4)
            {
                var v = (Vector4)value;
                return new Vector2(v.x, v.y);
            }
            if (value is Quaternion)
            {
                var q = (Quaternion)value;
                return new Vector2(q.x, q.y);
            }

            return Vector2.one * ToFloat(value);
        }
        #endregion

        #region ToVector3
        public static Vector3 ToVector3(float value) => new Vector3(value, value, value);
        public static Vector3 ToVector3(Vector2 vec) => new Vector3(vec.x, vec.y, 0);
        public static Vector3 ToVector3(Vector4 vec) => new Vector3(vec.x, vec.y, vec.z);
        public static Vector3 ToVector3(Quaternion vec) => new Vector3(vec.x, vec.y, vec.z);
        public static Vector3 ToVector3(object value)
        {
            if (value == null) return Vector3.zero;
            if (value is Vector2)
            {
                var v = (Vector2)value;
                return new Vector3(v.x, v.y, 0f);
            }
            if (value is Vector3)
            {
                return (Vector3)value;
            }
            if (value is Vector4)
            {
                var v = (Vector4)value;
                return new Vector3(v.x, v.y, v.z);
            }
            if (value is Quaternion)
            {
                var q = (Quaternion)value;
                return new Vector3(q.x, q.y, q.z);
            }
            if (value is Color)
            {
                var c = (Color)value;
                return new Vector3(c.r, c.g, c.b);
            }

            return Vector3.one * ToFloat(value);
        }
        #endregion

        #region ToVector4
        public static Vector4 ToVector4(float value) => new Vector4(value, value, value, value);
        public static Vector4 ToVector4(Vector2 vec) => new Vector4(vec.x, vec.y, 0f, 0f);
        public static Vector4 ToVector4(Vector3 vec) => new Vector4(vec.x, vec.y, vec.z, 0f);
        public static Vector4 ToVector4(Quaternion vec) => new Vector4(vec.x, vec.y, vec.z, vec.w);
        public static Vector4 ToVector4(object value)
        {
            if (value == null) return Vector4.zero;
            if (value is Vector2)
            {
                var v = (Vector2)value;
                return new Vector4(v.x, v.y, 0f, 0f);
            }
            if (value is Vector3)
            {
                var v = (Vector3)value;
                return new Vector4(v.x, v.y, v.z, 0f);
            }
            if (value is Vector4)
            {
                return (Vector4)value;
            }
            if (value is Quaternion)
            {
                var q = (Quaternion)value;
                return new Vector4(q.x, q.y, q.z, q.w);
            }
            if (value is Color)
            {
                var c = (Color)value;
                return new Vector4(c.r, c.g, c.b, c.a);
            }
            if (value is Rect)
            {
                var r = (Rect)value;
                return new Vector4(r.x, r.y, r.width, r.height);
            }

            return new Vector4(ToFloat(value), 0f);
        }
        #endregion

        #region ToEnum
        public static T ToEnum<T>(string val, T defaultValue) where T : Enum
        {
            try
            {
                T result = (T)System.Enum.Parse(typeof(T), val, true);
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }
        public static T ToEnum<T>(int val, T defaultValue) where T : Enum
        {
            try
            {
                return (T)System.Enum.ToObject(typeof(T), val);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static T ToEnum<T>(long val, T defaultValue) where T : Enum
        {
            try
            {
                return (T)System.Enum.ToObject(typeof(T), val);
            }
            catch
            {
                return defaultValue;
            }
        }
        public static T ToEnum<T>(object val, T defaultValue) where T : Enum => ToEnum<T>(System.Convert.ToString(val), defaultValue);
        public static T ToEnum<T>(string val) where T : Enum => ToEnum<T>(val, default(T));
        public static T ToEnum<T>(int val) where T : Enum => ToEnum<T>(val, default(T));
        public static T ToEnum<T>(object val) where T : Enum => ToEnum<T>(System.Convert.ToString(val), default(T));
        public static System.Enum ToEnumOfType(System.Type enumType, object value)
        {
            if (value == null)
                return System.Enum.ToObject(enumType, 0) as System.Enum;
            else if (value is int)
                return System.Enum.ToObject(enumType, ToInt(value)) as System.Enum;
            else
                return System.Enum.Parse(enumType, System.Convert.ToString(value), true) as System.Enum;
        }
        public static bool TryToEnum<T>(object value, out T result) where T : Enum
        {
            try
            {
                if (value == null)
                    result = (T)System.Enum.ToObject(typeof(T), 0);
                else if (value is int)
                    result = (T)System.Enum.ToObject(typeof(T), ToInt(value));
                else
                    result = (T)System.Enum.Parse(typeof(T), System.Convert.ToString(value), true);
                return true;
            }
            catch
            {
                result = default(T);
                return false;
            }
        }
        #endregion
    }
}
