using System.Collections;
using System.Reflection;

namespace UnityShared.Extensions.Unity3D
{
    public static class SerializedObjectExtensions
    {
        private static object GetFieldValue(string fieldName, object obj, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                return field.GetValue(obj);
            }
            return default(object);
        }

        private static object GetFieldValueWithIndex(string fieldName, object obj, int index, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                object list = field.GetValue(obj);
                if (list.GetType().IsArray)
                {
                    return ((object[])list)[index];
                }
                else if (list is IEnumerable)
                {
                    return ((IList)list)[index];
                }
            }
            return default(object);
        }

        public static bool SetFieldValue(string fieldName, object obj, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                field.SetValue(obj, value);
                return true;
            }
            return false;
        }

        public static bool SetFieldValueWithIndex(string fieldName, object obj, int index, object value, bool includeAllBases = false, BindingFlags bindings = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, bindings);
            if (field != null)
            {
                object list = field.GetValue(obj);
                if (list.GetType().IsArray)
                {
                    ((object[])list)[index] = value;
                    return true;
                }
                else if (value is IEnumerable)
                {
                    ((IList)list)[index] = value;
                    return true;
                }
            }
            return false;
        }
    }
}