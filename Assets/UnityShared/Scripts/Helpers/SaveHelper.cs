using System;
using UnityEngine;

namespace UnityShared.Helpers
{
    public class SaveHelper
    {
        public static bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public static float LoadFloat(string key) => PlayerPrefs.GetFloat(key);
        public static int LoadInt(string key) => PlayerPrefs.GetInt(key);
        public static bool LoadBoolean(string key) => Convert.ToBoolean(PlayerPrefs.GetInt(key));
        public static string LoadString(string key) => PlayerPrefs.GetString(key);
        public static T LoadEnum<T>(string key) where T : Enum => (T)Convert.ChangeType(PlayerPrefs.GetInt(key), typeof(T));

        public static void SaveFloat(string key, float value) => PlayerPrefs.SetFloat(key, value);
        public static void SaveInt(string key, int value) => PlayerPrefs.SetInt(key, value);
        public static void SaveString(string key, string value) => PlayerPrefs.SetString(key, value);
        public static void SaveBool(string key, bool value) => SaveInt(key, Convert.ToInt32(value));
        public static void SaveEnum<T>(string key, T value) => SaveInt(key, Convert.ToInt32(value));

        public static void ClearAll() => PlayerPrefs.DeleteAll();
    }
}