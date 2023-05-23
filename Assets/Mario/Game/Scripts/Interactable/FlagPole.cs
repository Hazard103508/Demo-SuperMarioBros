using Mario.Application.Services;
using Mario.Game.Interfaces;
using Mario.Game.Player;
using Unity.VisualScripting;
using UnityEngine;
using UnityShared.Behaviours.Various.Lerpers;

namespace Mario.Game.Interactable
{
    public class FlagPole : MonoBehaviour, ILeftHitable
    {
        [SerializeField] private GameObject _flag;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private LocalPositionLerper _flagLerper;

        private bool _isLowering;

        private void Awake()
        {
            _flagLerper.Speed = 1;
        }
        private void Start()
        {
            _flagLerper.Init();
        }

        public void LowerFlag()
        {
            if (!_isLowering)
            {
                _audioSource.Play();
                _isLowering = true;
                _flagLerper.RunForward();
                AllServices.GameDataService.IsMapCompleted = true;
            }
        }

        public void OnHitFromLeft(PlayerController player)
        {
            AllServices.TimeService.StopTimer();

            player.HoldFlagPole(transform.position.y);
            player.transform.position = new Vector3(transform.position.x - 0.5f, player.transform.position.y, player.transform.position.z);
            LowerFlag();
        }
    }
}