using Mario.Application.Components;
using Mario.Commons.UI;
using System.Collections;
using UnityEngine;

namespace Mario.Game.UI
{
    public class WorldLabel : ObjectPool
    {
        #region Objects
        [SerializeField] private IconText _label;
        #endregion

        #region Public Methods
        public void Show(string text, float time, float hight)
        {
            _label.Text = text;
            Vector3 goalPosition = transform.position + Vector3.up * hight;
            StartCoroutine(RiseLabel(goalPosition, time));
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseLabel(Vector3 GoalPosition, float time)
        {
            var _initPosition = transform.position;
            float _timer = 0;
            while (_timer < 1)
            {
                transform.position = Vector3.MoveTowards(_initPosition, GoalPosition, _timer);
                _timer += (Time.deltaTime / time);
                yield return null;
            }

            gameObject.SetActive(false);
        }
        #endregion
    }
}