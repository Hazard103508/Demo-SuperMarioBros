using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using Mario.Game.UI;
using System;
using UnityEngine;

namespace Mario.Application.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        #region Objects
        private IPoolService _poolService;

        [SerializeField] private PooledUIProfile scoreLabelPoolProfile;
        #endregion

        #region Properties
        public int Score { get; private set; }
        #endregion

        #region Events
        public event Action ScoreChanged;
        #endregion

        #region Public Methods
        public void Initalize()
        {
            _poolService = ServiceLocator.Current.Get<IPoolService>();
        }
        public void Dispose()
        {
        }
        public void Add(int points)
        {
            this.Score += points;
            ScoreChanged?.Invoke();

        }
        public void ShowPoints(int points, Vector3 initPosition, float time, float hight) => ShowLabel(points.ToString().PadLeft(4), initPosition, time, hight);
        public void Show1UP(Vector3 initPosition, float time, float hight) => ShowLabel("+", initPosition, time, hight);
        public void Reset()
        {
            Score = 0;
        }
        #endregion

        #region Private Methods
        private void ShowLabel(string text, Vector3 initPosition, float time, float hight)
        {
            var label = _poolService.GetObjectFromPool<WorldLabel>(scoreLabelPoolProfile, initPosition);
            label.Show(text, time, hight);
        }
        #endregion
    }
}