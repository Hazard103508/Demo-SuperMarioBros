using System.Collections;
using UnityEngine;
using UnityShared.Commons.Structs;

namespace UnityShared.Extensions.Unity3D
{
    public static class MonoBehaviourExtensions
    {
        public static CoroutineWrapper RunCoroutine(this MonoBehaviour owner, IEnumerator coroutine)
        {
            return new CoroutineWrapper(owner, coroutine);

            /* Example:
            * while (!TaskA(IsDone) && !TaskB(IsDone))
            *      Do something
            */
        }
    }
}