using Mario.Application.Interfaces;
using Mario.Game.ScriptableObjects.Pool;
using Mario.Game.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Mario.Application.Services
{
    public class ScoreService : MonoBehaviour, IScoreService
    {
        #region Objects
        [SerializeField] private ObjectPoolProfile scoreLabelPoolProfile;
        #endregion

        #region Properties
        public int Score { get; private set; }
        public UnityEvent OnScoreChanged { get; set; }
        #endregion

        #region Public Methods
        public void LoadService()
        {
            OnScoreChanged = new UnityEvent();
        }
        public void Add(int points)
        {
            this.Score += points;
            OnScoreChanged.Invoke();

        }
        public void ShowPoints(int points, Vector3 initPosition, float time, float hight) => ShowPoints(points, initPosition, time, hight, false);
        public void ShowPoints(int points, Vector3 initPosition, float time, float hight, bool isPerfament) => ShowLabel(points.ToString().PadLeft(4), initPosition, time, hight, isPerfament);
        public void Show1UP(Vector3 initPosition, float time, float hight) => ShowLabel("+", initPosition, time, hight, false);
        public void Reset()
        {
            Score = 0;
        }
        #endregion

        #region Private Methods
        private void ShowLabel(string text, Vector3 initPosition, float time, float hight, bool isPerfament)
        {
            var label = Services.PoolService.GetObjectFromPool<WorldLabel>(scoreLabelPoolProfile);
            label.transform.position = initPosition;
            label.Show(text, time, hight, isPerfament);
        }
        #endregion
    }
}