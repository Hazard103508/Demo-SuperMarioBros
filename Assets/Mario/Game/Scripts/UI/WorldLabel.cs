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
        private bool _isPerfament;
        #endregion

        #region Public Methods
        public void Show(string text, float time, float hight, bool isPerfament)
        {
            _label.Text = text;
            _isPerfament = isPerfament;
            Vector3 goalPosition = transform.position + Vector3.up * hight;
            StartCoroutine(RiseLabel(goalPosition, time));
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseLabel(Vector3 goalPosition, float time)
        {
            var _initPosition = transform.position;
            float _distance = goalPosition.y - _initPosition.y;
            float _timer = 0;
            while (_timer < 1)
            {
                float delta = _distance * _timer;
                transform.position = Vector3.MoveTowards(_initPosition, goalPosition, delta);
                _timer += (Time.deltaTime / time);
                yield return null;
            }

            if (!_isPerfament)
                gameObject.SetActive(false);
        }
        #endregion
    }
}