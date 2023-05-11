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

        public void IncreaseScore(int value, Vector3? positon = null)
        {
            gameDataProfile.Score += value;

            if (positon.HasValue)
            {
                TargetPoints point = Instantiate(targetPointsPrefab);
                point.transform.position = positon.Value + Vector3.up;
                point.SetPoints(value);
            }
        }
    }
}
