using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityShared.Behaviours.Triggers
{
    public class BaseTriggerSimple<TEnum> : MonoBehaviour where TEnum : Enum
    {
        [Serializable]
        /// <summary>
        /// UnityEvent class for Triggers.
        /// </summary>
        public class TriggerEvent : UnityEvent
        {
        }

        [Serializable]
        /// <summary>
        /// An Entry in the EventSystem delegates list.
        /// </summary>
        /// <remarks>
        /// It stores the callback and which event type should this callback be fired.
        /// </remarks>
        public class Entry
        {
            /// <summary>
            /// What type of event is the associated callback listening for.
            /// </summary>
            public TEnum eventID = default(TEnum);

            /// <summary>
            /// The desired TriggerEvent to be Invoked.
            /// </summary>
            public TriggerEvent callback = new TriggerEvent();
        }

        [FormerlySerializedAs("delegates")]
        [SerializeField]
        private List<Entry> m_Delegates;

        /// <summary>
        /// All the functions registered in this EventTrigger
        /// </summary>
        public List<Entry> triggers
        {
            get
            {
                if (m_Delegates == null)
                    m_Delegates = new List<Entry>();
                return m_Delegates;
            }
            set { m_Delegates = value; }
        }

        protected void Execute(TEnum id)
        {
            var triggerCount = triggers.Count;

            for (int i = 0, imax = triggers.Count; i < imax; ++i)
            {
                var ent = triggers[i];
                if (ent.eventID.Equals(id) && ent.callback != null)
                    ent.callback.Invoke();
            }
        }
    }
}
