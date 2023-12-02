using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class TestEventListener : MonoBehaviour
    {
        private ICoinService _coinService;
        private ISceneService _sceneService;
        private IPlayerService _playerService;

        [SerializeField] private PlayerController player;

        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
            _sceneService = ServiceLocator.Current.Get<ISceneService>();
            _playerService = ServiceLocator.Current.Get<IPlayerService>();
        }
        private void Update()
        {
            // TESTING----------------

            if (Input.GetKeyDown(KeyCode.R))
                _sceneService.LoadGameScene();

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                _playerService.AddLife();

            if (Input.GetKeyDown(KeyCode.G))
                _coinService.Add();


            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Break();
            // TESTING----------------
        }

        public void OnEventListener_Star() => player.ActivateSuperStar();
        public void OnEventListener_Buff() => player.Buff();
        public void OnEventListener_Nerf() => player.Nerf();
        public void OnEventListener_Kill() => player.Kill();
        public void OnEventListener_AddLife() => _playerService.AddLife();
        public void OnEventListener_AddCoin() => _coinService.Add();
    }
}