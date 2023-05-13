using JetBrains.Annotations;
using Mario.Game.Props;
using Mario.Game.ScriptableObjects;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityShared.Behaviours.Handlers;
using UnityShared.Patterns;

namespace Mario.Game.Handlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        public GameDataProfile gameDataProfile;
        


        protected override void Awake()
        {
            base.Awake();

            Camera.main.backgroundColor = gameDataProfile.WorldMapProfile.BackgroundColor;
        }
    }
}
