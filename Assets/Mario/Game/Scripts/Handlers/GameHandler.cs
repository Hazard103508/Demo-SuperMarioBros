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
        [SerializeField] private TargetPoints targetPointsPrefab;


        public bool AllowMove { get; private set; }

        protected override void Awake()
        {
            base.Awake();

            Camera.main.backgroundColor = gameDataProfile.WorldMapProfile.BackgroundColor;
            
            AllowMove = true;
        }

        public void ShowPoint(int value, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = Instantiate(targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight);
        }
        public void FreezeMove()
        {
            AllowMove = false;
        }
        public void ResumeMove()
        {
            AllowMove = true;
        }


    }
}
