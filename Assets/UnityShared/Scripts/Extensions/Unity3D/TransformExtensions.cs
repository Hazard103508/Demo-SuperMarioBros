using System.Linq;
using UnityEngine;

namespace UnityShared.Extensions.Unity3D
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Returns all transform childs
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static Transform[] GetAllChilds(this Transform transform) => Enumerable.Range(0, transform.childCount).Select(i => transform.GetChild(i)).ToArray();
    }
}

