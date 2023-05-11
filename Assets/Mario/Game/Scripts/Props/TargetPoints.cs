using Mario.Game.ScriptableObjects;
using System;
using UnityEngine;


namespace Mario.Game.Props
{
    public class TargetPoints : MonoBehaviour
    {
        [SerializeField] private TargetPointsProfile profile;
        [SerializeField] private SpriteRenderer[] _numberRenders;

        public void SetPoints(int point)
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
        }
    }

}