using System.Collections.Generic;

namespace UnityShared.Extensions.CSharp
{
    public static class IListExtensions
    {
        /// <summary>
        /// Gets a random item from the list
        /// </summary>
        /// <typeparam name="T">list data type</typeparam>
        /// <param name="list">list to search</param>
        /// <returns></returns>
        public static T GetRandomElements<T>(this IList<T> list)
        {
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        /// <summary>
        /// Get a random item from the list then remove it
        /// </summary>
        /// <typeparam name="T">list data type</typeparam>
        /// <param name="list">list to search</param>
        /// <returns></returns>
        public static T GetRandomElementsThenRemove<T>(this IList<T> list)
        {
            if (list.Count > 0)
            {
                var i = GetRandomElements(list);
                list.Remove(i);
                return i;
            }
            else
                return default(T);
        }

        /// <summary>
        /// Mix the elements inside the list
        /// </summary>
        /// <typeparam name="T">list data type</typeparam>
        /// <param name="list">List to shuffle</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}