using Mario.Commons.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mario.Game.Interactable
{
    public class FlagScoreLabel : MonoBehaviour
    {
        #region Objects
        [SerializeField] private SpriteRenderer[] _digits;
        [SerializeField] private NumberSprite[] _numbers;
        private Dictionary<string, Sprite> _numbersDic;
        #endregion

        #region Unity Methods
        private void Awake()
        {
            _numbersDic = new Dictionary<string, Sprite>();
            _numbers.ForEach(n => _numbersDic.Add(n.number, n.Sprite));
        }
        #endregion

        #region Public Methods
        public void ShowPoint(int point)
        {
            string text = point.ToString().PadLeft(4);
            for (int i = 0; i < text.Length; i++)
            {
                string value = text[i].ToString();
                if (_numbersDic.ContainsKey(value))
                {
                    _digits[i].sprite = _numbersDic[value];
                    _digits[i].gameObject.SetActive(true);
                }
            }

            StartCoroutine(RiseLabel());
        }
        #endregion

        #region Private Methods
        private IEnumerator RiseLabel()
        {
            float _distance = 8;
            float _timer = 0;
            while (_timer < 1)
            {
                float delta = _distance * _timer;
                float y = Mathf.MoveTowards(0, _distance, delta);
                transform.localPosition = new Vector3(transform.localPosition.x, y);
                _timer += Time.deltaTime;
                yield return null;
            }
        }
        #endregion

        #region Structures
        [Serializable]
        public class NumberSprite
        {
            public string number;
            public Sprite Sprite;
        }
        #endregion
    }
}