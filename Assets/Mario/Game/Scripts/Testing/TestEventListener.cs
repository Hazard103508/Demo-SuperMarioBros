using Mario.Application.Interfaces;
using Mario.Application.Services;
using UnityEngine;

namespace Mario.Game.Player
{
    public class TestEventListener : MonoBehaviour
    {
        private ICoinService _coinService;

        [SerializeField] private PlayerController player;

        private void Awake()
        {
            _coinService = ServiceLocator.Current.Get<ICoinService>();
        }
        private void Update()
        {
            // TESTING----------------

            if (Input.GetKeyDown(KeyCode.R))
                Services.SceneService.LoadMapScene(0);

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
                Services.PlayerService.AddLife();

            if (Input.GetKeyDown(KeyCode.G))
                _coinService.Add();


            if (Input.GetKeyDown(KeyCode.Space))
                Debug.Break();
            // TESTING----------------
        }

        public void OnEventListener_Buff() => player.Buff();
        public void OnEventListener_Nerf() => player.Nerf();
        public void OnEventListener_Kill() => player.Kill();
        public void OnEventListener_AddLife() => Services.PlayerService.AddLife();
        public void OnEventListener_AddCoin() => _coinService.Add();
    }
}