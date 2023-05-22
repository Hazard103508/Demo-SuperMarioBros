using Mario.Game.ScriptableObjects.UI;
using UnityEngine;
using UnityShared.Behaviours.Various.Lerpers;

namespace Mario.Game.Environment
{
    public class TargetPoints : MonoBehaviour
    {
        [SerializeField] private TargetPointsProfile profile;
        [SerializeField] private SpriteRenderer[] _numberRenders;
        private LocalPositionLerper _lerper;

        private void Awake()
        {
            _lerper = GetComponent<LocalPositionLerper>();
            _lerper.onLerpCompleted.AddListener(OnRisingCompleted);
        }
        public void ShowPoints(int point, float time, float hight)
        {
            string txtPoint = point.ToString("D4");

            for (int i = 0; i < txtPoint.Length; i++)
            {
                char number = txtPoint[i];
                if (i == 0 && number == '0')
                    _numberRenders[i].enabled = false;
                else
                    _numberRenders[i].sprite = profile.Sprites[number];
            }

            _lerper.Speed = 1 / time;
            _lerper.GoalPosition = transform.position + Vector3.up * hight;
            _lerper.RunForward();
        }
        public void ShowLabel(Sprite sprite, float time, float hight)
        {
            _numberRenders[0].enabled = false;
            _numberRenders[1].sprite = sprite;

            _lerper.Speed = 1 / time;
            _lerper.GoalPosition = transform.position + Vector3.up * hight;
            _lerper.RunForward();
        }
        private void OnRisingCompleted() => Destroy(gameObject);
    }
}