using System.Collections.Generic;
using UnityEngine;
using UnityShared.Behaviours.GameEventListeners;

namespace UnityShared.ScriptableObjects.Events
{
    [CreateAssetMenu(fileName = "GameEventProfile", menuName = "ScriptableObjects/Events/Game Event (No parameters)", order = 0)]
    public class GameEventProfile : ScriptableObject
    {
        /// <summary>
        /// The list of listeners that this event will notify if it is raised.
        /// </summary>
        private readonly List<GameEventListener> eventListeners = new();

        public void Raise()
        {
            for (int i = eventListeners.Count - 1; i >= 0; i--)
                eventListeners[i].Raise();
        }

        public void RegisterListener(GameEventListener listener)
        {
            if (!eventListeners.Contains(listener))
                eventListeners.Add(listener);
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (eventListeners.Contains(listener))
                eventListeners.Remove(listener);
        }
    }
}