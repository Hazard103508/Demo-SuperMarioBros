using System;
using UnityEngine;

namespace Mario.Game.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerProfile", menuName = "ScriptableObjects/Game/PlayerProfile", order = 0)]
    public class PlayerProfile : ScriptableObject
    {
        public Movement walk;
        public Movement run;

        [Serializable]
        public class Movement
        {
            public float Acceleration;
            public float MaxSpeed;
            public float Deacceleration;
        }
    }
}