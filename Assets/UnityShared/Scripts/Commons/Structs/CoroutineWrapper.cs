using System;
using System.Collections;
using UnityEngine;

namespace UnityShared.Commons.Structs
{
    public class CoroutineWrapper : IEnumerator
    {
        public event Action<CoroutineWrapper> Completed;
        public bool IsDone { get; private set; }
        public bool MoveNext() => !IsDone;
        public object Current { get; }
        public void Reset() { }

        public CoroutineWrapper(MonoBehaviour owner, IEnumerator coroutine)
        {
            Current = owner.StartCoroutine(Wrap(coroutine));
        }

        private IEnumerator Wrap(IEnumerator coroutine)
        {
            yield return coroutine;
            IsDone = true;
            Completed?.Invoke(this);
        }
    }
}