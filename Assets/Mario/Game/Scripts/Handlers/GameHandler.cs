using Mario.Game.Props;
using Mario.Game.ScriptableObjects;
using UnityEngine;
using UnityShared.Behaviours.Handlers;
using UnityShared.Patterns;

namespace Mario.Game.Handlers
{
    public class GameHandler : Singleton<GameHandler>
    {
        [SerializeField] private GameDataProfile gameDataProfile;
        [SerializeField] private TargetPoints targetPointsPrefab;
        private PauseHandler pauseHandler;

        public bool AllowMove { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            pauseHandler =  GetComponent<PauseHandler>();
            gameDataProfile.Score = 0;
            gameDataProfile.Coins = 0;
            AllowMove = true;
        }

        public void IncreaseScore(int value) => gameDataProfile.Score += value;
        public void IncreaseCoin(int value) => gameDataProfile.Coins += value;
        public void ShowPoint(int value, Vector3 initPosition, float time, float hight)
        {
            TargetPoints point = Instantiate(targetPointsPrefab, initPosition, Quaternion.identity);
            point.ShowPoints(value, time, hight);
        }
        public void FreezeMove() => AllowMove = false;
        public void ResumeMove() => AllowMove = true;
    }
}
