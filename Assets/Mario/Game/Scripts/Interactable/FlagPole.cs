using UnityEditor.Media;
using UnityEngine;
using UnityShared.Behaviours.Various.Lerpers;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour
    {
        [SerializeField] private GameObject _flag;
        [SerializeField] private LocalPositionLerper _flagLerper;

        private void Awake()
        {
            _flagLerper.Speed = 1;
            
        }
        private void Start()
        {
            _flagLerper.Init();
        }

        public void LowerFlag() => _flagLerper.RunForward();
    }
}