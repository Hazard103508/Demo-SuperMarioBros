using Mario.Game.Props;
using Mario.Game.ScriptableObjects;
using UnityEngine;
using UnityShared.Patterns;

namespace Mario.Game.Handlers
{
    public class GameDataHandler : Singleton<GameDataHandler>
    {
        [SerializeField] private GameDataProfile gameDataProfile;
        [SerializeField] private TargetPoints targetPointsPrefab;


        protected override void Awake()
        {
            base.Awake();
            gameDataProfile.Score = 0;
        }

        public void IncreaseScore(int value) => gameDataProfile.Score += value;
        public void IncreaseCoin(int value) => gameDataProfile.Coins += value;
        public void ShowPoint(int value, Vector3 positon)
        {
            TargetPoints point = Instantiate(targetPointsPrefab);
            point.transform.position = positon;
            point.SetPoints(value);
        }
    }
}
