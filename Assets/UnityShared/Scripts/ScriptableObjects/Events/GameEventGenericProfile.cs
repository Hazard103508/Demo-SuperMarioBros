using System.Collections.Generic;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;

namespace UnityShared.ScriptableObjects.Events
{
    public class GameEventGenericProfile<T> : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListenerGeneric<T>> eventListeners = new();

        public T value;

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].Raise(this.value);
        }

        public void RegisterListener(GameEventListenerGeneric<T> listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListenerGeneric<T> listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}