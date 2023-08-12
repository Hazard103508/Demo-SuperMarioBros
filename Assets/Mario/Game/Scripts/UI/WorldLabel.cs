using Mario.Commons.UI;
using System.Collections;
using UnityEngine;

namespace Mario.Game.UI
{
    public class WorldLabel : MonoBehaviour
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
        private IEnumerator RiseLabel(Vector3 goalPosition, float time)
        {
            var _initPosition = transform.position;
            float _distance = goalPosition.y - _initPosition.y;
            float _timer = 0;
            while (_timer < 1)
            {
                float delta = _distance * _timer;
                float y = Mathf.MoveTowards(_initPosition.y, goalPosition.y, delta);
                transform.position = new Vector3(transform.position.x, y);
                _timer += (Time.deltaTime / time);
                yield return null;
            }

            gameObject.SetActive(false);
        }
        #endregion
    }
}