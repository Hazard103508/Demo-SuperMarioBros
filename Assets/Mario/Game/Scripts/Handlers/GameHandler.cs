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
        [SerializeField] private GameDataProfile gameDataProfile;
        [SerializeField] private TargetPoints targetPointsPrefab;
        //private PauseHandler pauseHandler;

        private float _timer;

        public bool AllowMove { get; private set; }
        public bool AllowCountdown { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            //pauseHandler = GetComponent<PauseHandler>();
            gameDataProfile.Init();
            Camera.main.backgroundColor = gameDataProfile.WorldMapProfile.BackgroundColor;
            
            AllowMove = true;
            AllowCountdown = true;
        }
        private void Update()
        {
            UpdateTime();
        }

        public void IncreaseScore(int value) => gameDataProfile.Score += value;
        public void IncreaseCoin(int value) => gameDataProfile.Coins += value;
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = Instantiate(targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight);
        }
        public void FreezeMove()
        {
            AllowMove = false;
            AllowCountdown = false;
        }
        public void ResumeMove()
        {
            AllowMove = true;
            AllowCountdown = true;
        }

        private void UpdateTime()
        {
            if (AllowCountdown)
            {
                _timer += Time.deltaTime * 2.5f;
                gameDataProfile.Timer = Mathf.Max(0,gameDataProfile.WorldMapProfile.Time - (int)_timer);
            }
        }
    }
}
