using Mario.Application.Components;
using Mario.Game.ScriptableObjects.UI;
using System.Collections;
using UnityEngine;

namespace Mario.Game.Environment
{
    public class TargetPoints : ObjectPool
    {
        #region Objects
        [SerializeField] private TargetPointsProfile profile;
        [SerializeField] private SpriteRenderer[] _numberRenders;
        private bool _deactivateOnCompleted;
        #endregion

        #region Unity Methods
        protected override void Awake()
        {

        }
        #endregion

        #region Public Methods
        public void ShowPoints(int point, float time, float hight, bool deactivateOnCompleted)
        {
            _deactivateOnCompleted = deactivateOnCompleted;
            string txtPoint = point.ToString("D4");

            for (int i = 0; i < txtPoint.Length; i++)
            {
                char number = txtPoint[i];
                if (i == 0 && number == '0')
                    _numberRenders[i].enabled = false;
                else
                {
                    _numberRenders[i].enabled = true;
                    _numberRenders[i].sprite = profile.Sprites[number];
                }
            }

            Vector3 GoalPosition = transform.position + Vector3.up * hight;
            StartCoroutine(RiseLabel(GoalPosition, time));
        }
        public void ShowLabel(Sprite sprite, float time, float hight, bool deactivateOnCompleted)
        {
            _deactivateOnCompleted = deactivateOnCompleted;
            _numberRenders[0].enabled = false;
            _numberRenders[2].enabled = false;
            _numberRenders[3].enabled = false;
            _numberRenders[1].sprite = sprite;

            Vector3 GoalPosition = transform.position + Vector3.up * hight;
            StartCoroutine(RiseLabel(GoalPosition, time));
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseLabel(Vector3 GoalPosition, float time)
        {
            var _initPosition = transform.localPosition;
            float _timer = 0;
            while (_timer < 1)
            {
                transform.localPosition = Vector3.MoveTowards(_initPosition, GoalPosition, _timer);
                _timer += (Time.deltaTime / time);
                yield return null;
            }

            if (_deactivateOnCompleted)
                gameObject.SetActive(false);
        }
        #endregion
    }
}